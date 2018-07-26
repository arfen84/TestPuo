using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestPumox.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace TestPumox.Controllers
{
    public class HomeController : Controller
    {
        static List<SmartPhone> smartPhones;
        private readonly IHostingEnvironment _appEnvironment;
        public HomeController(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;

            smartPhones = new List<SmartPhone>();
            SmartPhone smartPhone;
            String[] line, fileLines;
            float tempFloat; int ram; int memory; int batery;

            fileLines = System.IO.File.ReadAllLines(Path.Combine(_appEnvironment.WebRootPath, "") + "/Data.csv");
            foreach (string fullLine in fileLines)
            {
                smartPhone = new SmartPhone();
                line = fullLine.Split(';');
                smartPhone.Name = line[0];
                smartPhone.System = line[1];
                float.TryParse(line[2].Replace(".", ","), out tempFloat);
                smartPhone.Diagonal = tempFloat;
                smartPhone.Resolution = line[3];
                Int32.TryParse(line[5], out ram);
                smartPhone.Ram = ram;
                Int32.TryParse(line[6], out memory);
                smartPhone.Memory = memory;
                Int32.TryParse(line[7], out batery);
                smartPhone.Batery = batery;

                HomeController.smartPhones.Add(smartPhone);
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult LoadData(string name = null, float diagonalA = 0, float diagonalB = 1000, int ramA = 0, int ramB = 100000, int memoryA = 0, int memoryB = 1000000, int bateryA = 0, int bateryB = 100000, string[] resolutions = null)
        {
            List<SmartPhone> toResturnList = new List<SmartPhone>();
            foreach (var smartphone in smartPhones)
            {
                if ((smartphone.Diagonal==0 )||(smartphone.Diagonal >= diagonalA && smartphone.Diagonal <= diagonalB))
                    if ((smartphone.Ram==0)||(smartphone.Ram >= ramA && smartphone.Ram <= ramB))
                        if ((smartphone.Memory==0)||(smartphone.Memory >= memoryA && smartphone.Memory <= memoryB))
                            if ((smartphone.Batery==0) || (smartphone.Batery >= bateryA && smartphone.Batery <= bateryB))

                                if (resolutions.Length > 0)
                                {
                                    foreach (var resol in resolutions)
                                    {
                                        if (smartphone.Resolution == String.Empty)
                                        {
                                            if (name != null && name != String.Empty)
                                            {
                                                if (smartphone.Name.Contains(name))
                                                    toResturnList.Add(smartphone);
                                                break;
                                            }
                                            else
                                            {
                                                toResturnList.Add(smartphone);
                                                break;
                                            }
                                        }
                                        if (smartphone.Resolution.Contains(resol))
                                        {
                                            if (name != null && name != String.Empty)
                                            {
                                                if (smartphone.Name.Contains(name))
                                                toResturnList.Add(smartphone);
                                            }
                                            else
                                            {
                                                toResturnList.Add(smartphone);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (name != null && name != String.Empty)
                                    {
                                        if (smartphone.Name.Contains(name))
                                            toResturnList.Add(smartphone);
                                    }
                                    else
                                    {
                                        toResturnList.Add(smartphone);
                                    }
                                    
                                }
           

        }


            return Json(JsonConvert.SerializeObject(toResturnList));
    }
}
}
