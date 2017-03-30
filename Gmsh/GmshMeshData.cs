using System.Collections.Generic;

namespace Gmsh
{
    /// <summary>
    /// Class to hold Mesh data (both nodes and elements)
    /// </summary>
    public class GmshMeshData
    {
        #region Private fields
        /// <summary>
        /// The values of the components
        /// </summary>
        private List<double> _components;
        /// <summary>
        /// The id of the entity (node or element) to which the data belongs
        /// </summary>
        private int _id;
        #endregion

        #region Public properties
        public int NumComponents {  get { return _components.Count; } }
        public List<double> Components { get { return _components; } }
        public int ID {  get { return _id; } }
        public System.Globalization.CultureInfo Format = new System.Globalization.CultureInfo("en-US");
        #endregion

        #region Constructor
        public GmshMeshData(int id = 0)
        {
            _id = id;
            _components = new List<double>();
        }
        public GmshMeshData(int id, List<double> components)
        {
            _id = id;
            _components = new List<double>();
            components.ForEach(c => _components.Add(c));
        }
        #endregion

        #region Public methods
        public override string ToString()
        {
            string s = ID.ToString(Format);
            _components.ForEach(c => { s += " " + c.ToString(Format); });
            return s;
        }

        public void SetComponents(List<double> d)
        {
            _components.Clear();
            d.ForEach(data => _components.Add(data));
        }

        public void SetComponents(double[] d)
        {
            _components.Clear();

            foreach(var data in d)
            {
                _components.Add(data);
            }
        }

        public void InsertNextComponent(double d)
        {
            _components.Add(d);
        }
        #endregion

        #region Private methods
        #endregion
    }
}
