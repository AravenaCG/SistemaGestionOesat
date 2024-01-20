namespace SistemaGestionOrquesta.Models.Middleware
{
    public class CustomResponses<TDto>
    {
        public Dictionary<string, TDto> dtoDictionary { get; set; }

        public List<Message> Messages { get; set; }

        public void AddDto(string propertyName, TDto dto)
        {
            dtoDictionary[propertyName] = dto;
        }

        public TDto GetDto(string propertyName)
        {
            if (dtoDictionary.ContainsKey(propertyName))
            {
                return dtoDictionary[propertyName];
            }
            return default;
        }

        public CustomResponses()
        {
            dtoDictionary = new Dictionary<string, TDto>();
            Messages = new List<Message>();
        }

    }

}
