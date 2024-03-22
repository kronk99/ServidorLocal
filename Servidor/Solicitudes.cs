using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
namespace Tiposdeplatos
{
    
    [ApiController] //define a la clase de abajo como controlador
    //de api aspnetcore
    [Route("platillo")]//define las rutas hacia donde hacer las solicitudes
    //get, set etc, comienzan con esto
    public class solicitudes:ControllerBase{
        [HttpGet]//se va a hacer un get
        [Route("getPlatillos")] //en la ruta getPlatillos se haran 
        //las solicitudes tipo Get
        public ActionResult<IEnumerable<platilloTemplate>> Get(){//obtiene
        //del servidor
            string filePath = "tipoplatillo.json";
            string jsonText = System.IO.File.ReadAllText(filePath);//lee todo el archivo
            List<platilloTemplate> registros;
            registros = JsonConvert.DeserializeObject<List<platilloTemplate>>(jsonText);
            foreach (var registro in registros){
                Console.WriteLine($"Tipo: {registro.Tipo}, Descripcion: {registro.Descripcion}");
                }
            Console.WriteLine("Datos serializados:");
            Console.WriteLine(JsonConvert.SerializeObject(registros));
            return Ok(JsonConvert.SerializeObject(registros));
            //hay que mandarselo directamente como un json serializado
            //consultar si esto lo manda como string o como json
        }
        [HttpPost]
        [Route("agregarPlatillos")]
        //crear un nuevo platillo 
        public ActionResult guardarPlatillos([FromBody] string nombrePlatillos){
            platilloTemplate json = JsonConvert.DeserializeObject<platilloTemplate>(nombrePlatillos);
            string filePath = "tipoplatillo.JSON";
            string jsonText = System.IO.File.ReadAllText(filePath);//lee todo el archivo
            List<platilloTemplate> registros = JsonConvert.DeserializeObject<List<platilloTemplate>>(jsonText);
            registros.Add(json);
            string jsonaGuardar= JsonConvert.SerializeObject(registros, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, jsonaGuardar);
            Console.WriteLine("JSON guardado en el archivo:");
            return Ok();
            }
    }
}