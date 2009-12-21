using System;
using System.Linq;
using Xunit;
using System.Reflection;

namespace DotNetKoans.KoanRunner
{
    class Program
    {
        static int TEST_FAILED = 0;

        static int Main(string[] args)
        {
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
                Run("DotNetKoans.CSharp.AboutAsserts", koans, wrapper);
                Run("DotNetKoans.CSharp.AboutNil", koans, wrapper);
                Run("DotNetKoans.CSharp.AboutArrays", koans, wrapper);
                //Run("DotNetKoans.CSharp.AboutArrayAssignment", koans, wrapper);
                //Run("DotNetKoans.CSharp.AboutHashes", koans, wrapper);
                //Run("DotNetKoans.CSharp.AboutStrings", koans, wrapper);
                //Run("DotNetKoans.CSharp.AboutMethods", koans, wrapper);
                //Run("DotNetKoans.CSharp.AboutControlStatements", koans, wrapper);
                //Run("DotNetKoans.CSharp.AboutTrueAndFalse", koans, wrapper);
                //Run("DotNetKoans.CSharp.AboutTriangleProject", koans, wrapper);
                //Run("DotNetKoans.CSharp.AboutExceptions", koans, wrapper);
                //Run("DotNetKoans.CSharp.AboutTriangleProject2", koans, wrapper);
                //Run("DotNetKoans.CSharp.AboutIteration", koans, wrapper);
                //Run("DotNetKoans.CSharp.AboutBlocks", koans, wrapper);
                //Run("DotNetKoans.CSharp.AboutSandwichCode", koans, wrapper);
                //Run("DotNetKoans.CSharp.AboutScoringProject", koans, wrapper);
                //Run("DotNetKoans.CSharp.AboutClasses", koans, wrapper);
                //Run("DotNetKoans.CSharp.AboutDiceProject", koans, wrapper);
                //Run("DotNetKoans.CSharp.AboutInheritance", koans, wrapper);
                //Run("DotNetKoans.CSharp.AboutModules", koans, wrapper);
                //Run("DotNetKoans.CSharp.AboutScope", koans, wrapper);
                //Run("DotNetKoans.CSharp.AboutClassMethods", koans, wrapper);
                //Run("DotNetKoans.CSharp.AboutMessagePassing", koans, wrapper);
                //Run("DotNetKoans.CSharp.AboutProxyObjectProject", koans, wrapper);
                //Run("DotNetKoans.CSharp.AboutExtraCredit", koans, wrapper);

                //wrapper.RunClass("DotNetKoans.CSharp.AboutAsserts", callback);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Karma has killed the runner. Exception was: " + ex.ToString());
                return -1;
            }
            Console.WriteLine("*******************************************************************");
            Console.WriteLine("*******************************************************************");
            Console.WriteLine("");
            Console.WriteLine("");
            return TEST_FAILED;
        }

        static void Run(string className, System.Reflection.Assembly koanAssembly, ExecutorWrapper wrapper)
        {
            
            Type classToRun = koanAssembly.GetType(className);
            string[] queue = new string[classToRun.GetMethods().Length + 1];
            foreach (MethodInfo method in classToRun.GetMethods())
            {
                if (method.Name == null) { continue; }
                DotNetKoans.KoanAttribute custAttr = method.GetCustomAttributes(typeof(DotNetKoans.KoanAttribute), false).FirstOrDefault() as DotNetKoans.KoanAttribute;
                if (custAttr == null) { continue; }
                queue[custAttr.Position] = method.Name;
            }
            foreach (string test in queue)
            {
                if (String.IsNullOrEmpty(test)) { continue; }
                if (TEST_FAILED != 0) { continue; }
                wrapper.RunTest(className, test, callback);
            }
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
