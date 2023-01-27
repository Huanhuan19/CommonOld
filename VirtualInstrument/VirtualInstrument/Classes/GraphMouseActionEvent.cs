using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VirtualInstrument.Classes
{
    public class GraphMouseActionEventArgs :EventArgs
    {
        public GraphMouseActionEventArgs(Classes.GraphicsSelectType selectType, List<Classes.GraphLine> selectedCurves)
        {
            _selectType = selectType;
            _selectedCurves.AddRange(selectedCurves);
        }
        #region Props
        Classes.GraphicsSelectType _selectType;
        List<Classes.GraphLine> _selectedCurves = new List<GraphLine>();
        public Classes.GraphicsSelectType SelectedType
        {
            get { return _selectType; }
        }
        public List<Classes.GraphLine> SelectedCurves
        {
            get { return _selectedCurves; }
        }
        #endregion
    }

    public delegate void GraphMouseActionHandler(object sender,GraphMouseActionEventArgs e );
}
