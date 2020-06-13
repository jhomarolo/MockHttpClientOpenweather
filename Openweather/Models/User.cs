using Newtonsoft.Json;
using System.Collections.Generic;

namespace Openweather.Models
{
    public class User
    {
        public int Id;
        /// <summary>
        /// Holds first name of the user
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Holds lasatname of the user
        /// </summary>
        public string LastName { get; set; }
    }
}
