using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections
{
    // IListableNames - static
    public abstract class CrossSection : DefinitionElement
    {
        /// <summary>
        /// If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; protected set; }

        /// <summary>
        /// Gets or sets the material.
        /// </summary>
        /// <value>The material.</value>
        public Material Material { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrossSection"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected CrossSection(string name) : base(name)
        {
        }



        #region API Functions
        #endregion
    }
}
