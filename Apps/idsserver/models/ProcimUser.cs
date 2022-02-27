namespace idsserver
{
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Defines the <see cref="ProcimUser" />.
    /// </summary>
    public class ProcimUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the CompanyId.
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the LanguageId.
        /// </summary>
        public int LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the AdminTypeId.
        /// </summary>
        public int AdminTypeId { get; set; }

        /// <summary>
        /// Gets or sets the UserTypeId.
        /// </summary>
        public int UserTypeId { get; set; }
    }
}
