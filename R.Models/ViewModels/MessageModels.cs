using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Numerics;

namespace R.Models.ViewModels
{
   
    public class ApiResponse
    {
        [JsonPropertyName("response")]
        public string Response { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }

        //[JsonPropertyName("counties")]
        //public List<Country> Counties { get; set; }

        //[JsonPropertyName("numbers")]
        //public List<NumberInfo> Numbers { get; set; }

        [JsonPropertyName("messages")]
        public MessagesData Messages { get; set; }
    }

    public class Country
    {
        [JsonPropertyName("country")]
        public int CountryCode { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("online")]
        public bool Online { get; set; }

        [JsonPropertyName("locale")]
        public string Locale { get; set; }
    }

    public class NumberInfo
    {
        [JsonPropertyName("country")]
        public int Country { get; set; }

        [JsonPropertyName("data_humans")]
        public string DataHumans { get; set; }

        [JsonPropertyName("full_number")]
        public string FullNumber { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("is_archive")]
        public bool IsArchive { get; set; }
    }

    public class MessagesData
    {
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }

        [JsonPropertyName("data")]
        public List<Message> Data { get; set; }
    }

    public class Message
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("in_number")]
        public string InNumber { get; set; }

        [JsonPropertyName("my_number")]
        public long MyNumber { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("data_humans")]
        public string DataHumans { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }


        public string LastFourDigits { get; set; }
        public int DurationSince { get; set; }
    }

}
