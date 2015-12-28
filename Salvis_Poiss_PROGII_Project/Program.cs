using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;

namespace Salvis_Poiss_PROGII_Project
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Nodrošina DLL failu iekļaušanu programmas EXE failā.
            // Uz EXE izmēra rēķina (palielinās) atbrīvojamies no 7 "liekiem" DLL failiem, kas atrastos blakus mūsu izpildāmajam failam.
            // Avots: http://sanganakauthority.blogspot.com/2012/03/creating-single-exe-that-depends-on.html
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            Application.Run(new frmMain());
        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            //This handler is called only when the common language runtime tries to bind to the assembly and fails.
            //Retrieve the list of referenced assemblies in an array of AssemblyName.
            Assembly objExecutingAssemblies;
            Byte[] assemblyData = null;

            objExecutingAssemblies = Assembly.GetExecutingAssembly();
            AssemblyName[] arrReferencedAssmbNames = objExecutingAssemblies.GetReferencedAssemblies();

            //Loop through the array of referenced assembly names.
            foreach (AssemblyName strAssmbName in arrReferencedAssmbNames)
            {
                //Check for the assembly names that have raised the "AssemblyResolve" event.
                if (strAssmbName.FullName.Substring(0, strAssmbName.FullName.IndexOf(",")) == args.Name.Substring(0, args.Name.IndexOf(",")))
                {
                    //Build the path of the assembly from where it has to be loaded.                                               
                    //“Salvis_Poiss_PROGII_Project” is the name of namespace and “dll” is the
                    //name of folder where DLL files to be referenced is present.
                    var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Salvis_Poiss_PROGII_Project.dll." + new AssemblyName(args.Name).Name + ".dll");

                    assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    break;
                }
            }
            //Return the loaded assembly.
            return Assembly.Load(assemblyData);
        }
    }
}
