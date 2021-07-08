using System.Text.Json;

namespace IMC.Taxes.Api.Serialization
{
    public class SnakeCaseNamingPolicy: JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            return name.ToSnakeCase();
        }
    }
}