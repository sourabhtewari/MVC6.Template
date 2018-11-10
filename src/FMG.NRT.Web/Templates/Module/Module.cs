using Genny;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FMG.NRT.Web.Templates
{
    [GennyModuleDescriptor("Default system module template")]
    public class Module : GennyModule
    {
        [GennyParameter(0, Required = true)]
        public String Model { get; set; }

        [GennyParameter(1, Required = true)]
        public String Controller { get; set; }

        [GennyParameter(2, Required = false)]
        public String Area { get; set; }

        public Module(IServiceProvider services)
            : base(services)
        {
        }

        public override void Run()
        {
            String path = (Area != null ? Area + "/" : "") + Controller;
            Dictionary<String, GennyScaffoldingResult> results = new Dictionary<String, GennyScaffoldingResult>();

            results.Add($"../FMG.NRT.Controllers/{path}/{Controller}Controller.cs", Scaffold("Controllers/Controller"));
            results.Add($"../../test/FMG.NRT.Tests/Unit/Controllers/{path}/{Controller}ControllerTests.cs", Scaffold("Tests/ControllerTests"));

            results.Add($"../FMG.NRT.Objects/Models/{path}/{Model}.cs", Scaffold("Objects/Model"));
            results.Add($"../FMG.NRT.Objects/Views/{path}/{Model}View.cs", Scaffold("Objects/View"));

            results.Add($"../FMG.NRT.Services/{path}/{Model}Service.cs", Scaffold("Services/Service"));
            results.Add($"../FMG.NRT.Services/{path}/I{Model}Service.cs", Scaffold("Services/IService"));
            results.Add($"../../test/FMG.NRT.Tests/Unit/Services/{path}/{Model}ServiceTests.cs", Scaffold("Tests/ServiceTests"));

            results.Add($"../FMG.NRT.Validators/{path}/{Model}Validator.cs", Scaffold("Validators/Validator"));
            results.Add($"../FMG.NRT.Validators/{path}/I{Model}Validator.cs", Scaffold("Validators/IValidator"));
            results.Add($"../../test/FMG.NRT.Tests/Unit/Validators/{path}/{Model}ValidatorTests.cs", Scaffold("Tests/ValidatorTests"));

            results.Add($"../FMG.NRT.Web/Views/{path}/Index.cshtml", Scaffold("Web/Index"));
            results.Add($"../FMG.NRT.Web/Views/{path}/Create.cshtml", Scaffold("Web/Create"));
            results.Add($"../FMG.NRT.Web/Views/{path}/Details.cshtml", Scaffold("Web/Details"));
            results.Add($"../FMG.NRT.Web/Views/{path}/Edit.cshtml", Scaffold("Web/Edit"));
            results.Add($"../FMG.NRT.Web/Views/{path}/Delete.cshtml", Scaffold("Web/Delete"));

            if (results.Any(result => result.Value.Errors.Any()))
            {
                Dictionary<String, GennyScaffoldingResult> errors = new Dictionary<String, GennyScaffoldingResult>(results.Where(x => x.Value.Errors.Any()));

                Write(errors);

                Logger.WriteLine("");
                Logger.WriteLine("Scaffolding failed! Rolling back...", ConsoleColor.Red);
            }
            else
            {
                Logger.WriteLine("");

                TryWrite(results);

                Logger.WriteLine("");
                Logger.WriteLine("Scaffolded successfully!", ConsoleColor.Green);
            }
        }

        public override void ShowHelp()
        {
            Logger.WriteLine("Parameters:");
            Logger.WriteLine("    1 - Scaffolded model.");
            Logger.WriteLine("    2 - Scaffolded controller.");
            Logger.WriteLine("    3 - Scaffolded area (optional).");
        }

        private GennyScaffoldingResult Scaffold(String path)
        {
            return Scaffolder.Scaffold("Templates/Module/" + path, new ModuleModel(Model, Controller, Area));
        }
    }
}
