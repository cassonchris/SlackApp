using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SlackApp.Models
{
    public class AppInstall
    {
        [Required]
        [StringLength(250)]
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [StringLength(250)]
        [JsonProperty("scope")]
        public string Scope { get; set; }

        [Key]
        [Required]
        [StringLength(50)]
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [StringLength(250)]
        [JsonProperty("team_name")]
        public string TeamName { get; set; }

        [StringLength(50)]
        [JsonProperty("team_id")]
        public string TeamId { get; set; }

        [JsonProperty("ok")]
        public bool Ok { get; set; }
    }
}
