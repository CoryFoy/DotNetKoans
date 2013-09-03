using System;
using System.Linq;
using Xunit;
using System.Reflection;
using System.Text;

namespace DotNetKoans.KoanRunner
{
	class Program
	{
		static int TEST_FAILED = 0;

		static int Main(string[] args)
		{
			StringBuilder progress = new StringBuilder();
			try
			{
				Console.WriteLine("");
				Console.WriteLine("");
				Console.WriteLine("*******************************************************************");
				Console.WriteLine("*******************************************************************");
				string koan_path = args[0];
				Xunit.ExecutorWrapper wrapper = new ExecutorWrapper(koan_path, null, false);
				System.Reflection.Assembly koans = System.Reflection.Assembly.LoadFrom(koan_path);
				if (koans == null) { Console.WriteLine("Bad Assembly"); return -1; }
				Type pathType = null;
				foreach (Type type in koans.GetExportedTypes())
				{
					if (typeof(KoanHelpers.IAmThePathToEnlightenment).IsAssignableFrom(type))
					{
						pathType = type;
						break;
					}
				}

				KoanHelpers.IAmThePathToEnlightenment path = Activator.CreateInstance(pathType) as KoanHelpers.IAmThePathToEnlightenment;
				string[] thePath = path.ThePath;

				foreach (string koan in thePath)
				{
					progress.AppendFormat("{0},", Run(koan, koans, wrapper));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Karma has killed the runner. Exception was: " + ex.ToString());
				return -1;
			}
			Console.WriteLine("Koan progress:{0}", progress.ToString());
			Console.WriteLine("*******************************************************************");
			Console.WriteLine("*******************************************************************");
			Console.WriteLine("");
			Console.WriteLine("");
			return TEST_FAILED;
		}

        static string Run(string className, System.Reflection.Assembly koanAssembly, ExecutorWrapper wrapper)
        {
            
            Type classToRun = koanAssembly.GetType(className);

            if (classToRun == null) { return "(0/0)"; }

            string[] queue = new string[classToRun.GetMethods().Length + 1];
            int highestKoanNumber = 0;
            foreach (MethodInfo method in classToRun.GetMethods())
            {
                if (method.Name == null) { continue; }
                DotNetKoans.KoanAttribute custAttr = method.GetCustomAttributes(typeof(DotNetKoans.KoanAttribute), false).FirstOrDefault() as DotNetKoans.KoanAttribute;
                if (custAttr == null) { continue; }
                queue[custAttr.Position] = method.Name;
                if (custAttr.Position > highestKoanNumber) { highestKoanNumber = custAttr.Position; }
            }

            int numberOfTestsActuallyRun = 0;
			int numberOfTestsPassed = 0;
            foreach (string test in queue)
            {
                if (String.IsNullOrEmpty(test)) 
					continue;
                numberOfTestsActuallyRun++;
                if (TEST_FAILED != 0)
					continue;
                wrapper.RunTest(className, test, callback);
				if (TEST_FAILED == 0)
					numberOfTestsPassed++;
			}

            if (numberOfTestsActuallyRun != highestKoanNumber)
            {
                Console.WriteLine("!!!!WARNING - Some Koans appear disabled. The highest koan found was {0} but we ran {1} koan(s)",
                    highestKoanNumber, numberOfTestsActuallyRun);
            }
			return string.Format("({0}/{1})", numberOfTestsPassed, numberOfTestsActuallyRun);
        }

        static bool callback(System.Xml.XmlNode result)
        {
            bool KEEP_GOING = true;
            bool STOP_RUNNING = false;

            if (result.Name != "test") { return KEEP_GOING; }

            if (result.Attributes["result"].Value == "Fail")
            {
                Console.WriteLine("The test {0} has damaged your karma. The following stack trace has been declared to be at fault", result.Attributes["name"].Value);
                Console.WriteLine(result.SelectSingleNode("failure/message").InnerText);
                Console.WriteLine(result.SelectSingleNode("failure/stack-trace").InnerText);
                Console.WriteLine(result.OuterXml);
                TEST_FAILED = 1;
                return STOP_RUNNING;
            }
            else
            {
                Console.WriteLine("{0} has expanded your awareness", result.Attributes["name"].Value);
                return STOP_RUNNING;
            }
        }
    }
}
