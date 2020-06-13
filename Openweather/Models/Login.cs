namespace Openweather.Models
{
    public class Login
    {
        /// <summary>
        /// Holds userid for reference
        /// Reference will be created based on requirement
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// User name of the user
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Password of the user
        /// </summary>
        public string Password { get; set; }
    }
}
