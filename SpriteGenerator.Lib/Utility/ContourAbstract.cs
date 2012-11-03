using System.Collections.Generic;

namespace SpriteGenerator.Utility
{
    /// <summary>
    /// Contour is the list of the modules on the top (horizontal contour) or on the right (vertical contour) of the
    /// placement. It is needed for linear time computation of the modules coordinates. It is easier to understand 
    /// it from some figure. See reference.
    /// </summary>
    public abstract class ContourAbstract
    {
        protected void Construct(Module root)
        {
            ModuleSequence = new List<Module> {
                root
            };

            WhereMax = root;
            InsertationIndex = -1;
        }

        /// <summary>
        /// Sets the insertation index of the contour.
        /// </summary>
        public int InsertationIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the sequence of modules whereof the contour consists.
        /// </summary>
        public List<Module> ModuleSequence
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the module with the maximum y-coordinate in a given x-coordinate range or conversely.
        /// It is calculated by FindMax method.
        /// </summary>
        public Module WhereMax
        {
            get;
            set;
        }

        public abstract int FindMax(int to);

        /// <summary>
        /// Inserts new module into the contour and clears WhereMax value.
        /// </summary>
        /// <param name="module"></param>
        public void Update(Module module)
        {
            ModuleSequence.Insert(++InsertationIndex, module);
            WhereMax = new Module(-1, null, 0);
        }
    }
}
