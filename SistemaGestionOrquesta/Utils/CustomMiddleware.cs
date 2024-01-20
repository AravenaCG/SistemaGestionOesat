﻿using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SistemaGestionOrquesta.Models.Middleware
{

    public class CustomResponse<TDto> : ControllerBase
    {
        public TDto ResponseObject { get; set; }
        public string JsonResult { get; set; }
        public List<Message> Messages { get; set; }

        public CustomResponse()
        {
            Messages = new List<Message>();
            ResponseObject = default!;
            JsonResult = string.Empty;
        }

        public CustomResponse<TDto> BuildCustomResponse(TDto dto, string propertyName, string status, string help)
        {
            var customResponse = new CustomResponse<TDto>
            {
                ResponseObject = dto
            };

            customResponse.Messages = new List<Message>
        {
            new Message
            {
                status = status,
                text = "",
                help = help
            }
        };

            var jArrayMessages = JArray.FromObject(customResponse.Messages);
            var response = new JObject(
                new JProperty(propertyName, JObject.FromObject(customResponse.ResponseObject, new JsonSerializer
                {
                    Converters = { new GuidJsonConverter() }
                })),
                new JProperty("messages", jArrayMessages)
            );

            customResponse.JsonResult = response.ToString(Formatting.Indented);

            return customResponse;
        }

        public IActionResult HandleCustomResponse()
        {
            if (Messages.Any(message => message.status == "respuesta exitosa"))
            {
                return StatusCode(StatusCodes.Status201Created, this);
            }
            else if (Messages.Any(message => message.status == "Parámetros de entrada inválidos/mal escritos"))
            {
                return BadRequest(this);
            }
            else if (Messages.Any(message => message.status == "Timeout"))
            {
                return StatusCode(StatusCodes.Status504GatewayTimeout, this);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, this);
        }

        public CustomResponse<TDto> HandleDbUpdateException(DbUpdateException ex, string propertyName, string status, string help)
        {
            var customResponse = new CustomResponse<TDto>();

            if (ex.InnerException != null && ex.InnerException is System.Data.SqlClient.SqlException sqlException)
            {
                switch (sqlException.Number)
                {
                    case 2627: // Violación de clave única
                        customResponse = BuildCustomResponse(ResponseObject, propertyName, "Parámetros de entrada inválidos/mal escritos", "Violación de clave única");
                        break;
                    // Agrega más casos según sea necesario
                    default:
                        customResponse = BuildCustomResponse(ResponseObject, propertyName, "Error desconocido", "Ocurrió un error desconocido");
                        break;
                }
            }
            else
            {
                customResponse = BuildCustomResponse(ResponseObject, propertyName, ex.Message, ex.InnerException?.Message ?? string.Empty);
            }

            return customResponse;
        }
    }

    public class GuidJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Guid);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                if (Guid.TryParse((string)reader.Value, out Guid result))
                {
                    return result;
                }
            }

            return Guid.Empty; // Return Guid.Empty if unable to parse
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is Guid guid)
            {
                writer.WriteValue(guid.ToString());
            }
        }
    }


    public class Message
    {
        public string status { get; set; }
        public string text { get; set; }
        public string help { get; set; }
    }
}

    





