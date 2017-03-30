using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmsh
{
    public class GmshMeshNodeData
    {
        #region Private fields
        List<string> _stringTags;
        List<double> _realTags;
        List<int> _intTags;
        List<GmshMeshData> _data;
        bool _sane;
        #endregion

        #region Public properties
        public List<string> StringTags {  get { return _stringTags; } }
        public int NumStringTags {  get { return _stringTags.Count; } }
        public List<double> RealTags { get { return _realTags; } }
        public int NumRealTags { get { return _realTags.Count; } }
        public List<int> IntTags {  get { return _intTags; } }
        public int NumIntTags {  get { return _intTags.Count; } }
        public List<GmshMeshData> Data {  get { return _data; } }
        public int NumData {  get { return _data.Count; } }
        public bool Sane { get { return _sane; } }
        public System.Globalization.CultureInfo Format = new System.Globalization.CultureInfo("en-US");
        #endregion

        #region Constructor
        public GmshMeshNodeData(List<string> stringTags, List<double> realTags, List<int> intTags, List<GmshMeshData> data)
        {
            _initializeLists();
            stringTags.ForEach(st => _stringTags.Add(st));
            realTags.ForEach(rt => _realTags.Add(rt));
            intTags.ForEach(it => _intTags.Add(it));
            data.ForEach(d => _data.Add(d));

            _sanityCheck();
        }
        #endregion

        #region Public methods
        public List<string> GetStrings()
        {
            List<string> s = new List<string>();
            if (Sane)
            {
                string wrapper = "\"";
                s.Add(NumStringTags.ToString(Format));
                _stringTags.ForEach(st => s.Add(Wrap(st, wrapper)));
                s.Add(NumRealTags.ToString(Format));
                _realTags.ForEach(rt => s.Add(rt.ToString(Format)));
                s.Add(NumIntTags.ToString(Format));
                _intTags.ForEach(it => s.Add(it.ToString(Format)));
                _data.ForEach(d => s.Add(d.ToString()));
            }
            return s;
        }
        #endregion

        #region Private methods
        private void _initializeLists()
        {
            _stringTags = new List<string>();
            _realTags = new List<double>();
            _intTags = new List<int>();
            _data = new List<GmshMeshData>();
        }

        private void _sanityCheck()
        {
            _sane = NumIntTags >= 3;
            if (!_sane)
            {
                Console.WriteLine("GmshMeshNodeData: There must be at least 3 inteher tags.");
                return;
            }

            int numComponents = _intTags[1];
            int numNodes = _intTags[2];
            _sane = _data.All(d => d.NumComponents == numComponents);
            if (!_sane)
            {
                Console.WriteLine("GmshMeshNodeData: All data does not have the correct number of components.");
                return;
            }

            _sane = NumData == numNodes;
            if (!_sane)
            {
                Console.WriteLine("GmshMeshNodeData: The number of data does not match the number of nodes.");
            }
        }

        private string Wrap(string s, string wrapper)
        {
            return wrapper + s + wrapper;
        }
        #endregion
    }
}
