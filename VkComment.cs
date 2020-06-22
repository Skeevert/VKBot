namespace VKBot
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Comments
    {
        [JsonProperty("response")]
        public Response Response { get; set; }
    }

    public partial class Response
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("items")]
        public List<Comment> Items { get; set; }

        [JsonProperty("profiles")]
        public List<Profile> Profiles { get; set; }

        [JsonProperty("groups")]
        public List<object> Groups { get; set; }
    }

    public partial class Comment
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("from_id")]
        public long FromId { get; set; }

        [JsonProperty("date")]
        public long Date { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("can_edit")]
        public long CanEdit { get; set; }
    }

    public partial class Profile
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("is_closed")]
        public bool IsClosed { get; set; }

        [JsonProperty("can_access_closed")]
        public bool CanAccessClosed { get; set; }

        [JsonProperty("sex")]
        public long Sex { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("photo_50")]
        public Uri Photo50 { get; set; }

        [JsonProperty("photo_100")]
        public Uri Photo100 { get; set; }

        [JsonProperty("online")]
        public long Online { get; set; }

        [JsonProperty("online_app", NullValueHandling = NullValueHandling.Ignore)]
        public long? OnlineApp { get; set; }

        [JsonProperty("online_mobile", NullValueHandling = NullValueHandling.Ignore)]
        public long? OnlineMobile { get; set; }
    }
}
