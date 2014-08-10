namespace Urban.Data
{
    public partial class User
    {
        /// <summary>
        /// Gets the full name.
        /// </summary>
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}