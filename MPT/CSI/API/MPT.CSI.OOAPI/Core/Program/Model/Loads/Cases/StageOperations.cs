// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-05-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-06-2018
// ***********************************************************************
// <copyright file="StageOperations.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Loads.Definitions;
using ApiStaticNonlinearStaged = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.StaticNonlinearStaged;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Stage data associated with each stage definition.
    /// </summary>
    public class StageOperations
    {
        #region Fields & Properties
        /// <summary>
        /// The static nonlinear staged data object from the API.
        /// </summary>
        protected IStaticNonlinearStaged _apiStaticNonlinearStaged;

        /// <summary>
        /// List of stage data items.
        /// </summary>
        private List<StageOperation> _items = new List<StageOperation>();
        /// <summary>
        /// List of stage data items
        /// </summary>
        /// <value>The items.</value>
        public ReadOnlyCollection<StageOperation> Items => new ReadOnlyCollection<StageOperation>(_items);

        /// <summary>
        /// The name of the load case.
        /// </summary>
        /// <value>The name of the case.</value>
        public string CaseName { get; internal set; }

        /// <summary>
        /// The stage number associated with the list of operations.
        /// </summary>
        /// <value>The stage number.</value>
        public int StageNumber { get; internal set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="StageOperations" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="caseName">Name of the case.</param>
        /// <param name="stageNumber">The stage number.</param>
        public StageOperations(IStaticNonlinearStaged app, string caseName, int stageNumber)
        {
            _apiStaticNonlinearStaged = app;
            CaseName = caseName;
            StageNumber = stageNumber;
        }

        /// <summary>
        /// Returns stage operations for the specified stage in the load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillStageOperations()
        {
            if (_apiStaticNonlinearStaged == null) return;
            _items = new List<StageOperation>();
            _apiStaticNonlinearStaged.GetStageData(CaseName, StageNumber,
                out var operations,
                out var objectTypes,
                out var nameObjects,
                out var ages,
                out var loadOrObjectTypes,
                out var loadOrObjectNames,
                out var scaleFactors);

            for (int i = 0; i < operations.Length; i++)
            {
                StageOperation datum = new StageOperation
                {
                    Operation = operations[i],
                    ObjectType = objectTypes[i],
                    NameObject = nameObjects[i],
                    Age = ages[i],
                    LoadOrObjectType = loadOrObjectTypes[i],
                    LoadOrObjectName = loadOrObjectNames[i],
                    ScaleFactor = scaleFactors[i]
                };

                _items.Add(datum);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the stage operation by index.
        /// </summary>
        /// <param name="index">The index to return.</param>
        /// <returns>StageOperation.</returns>
        public StageOperation GetOperationAt(int index)
        {
            return _items[index];
        }

        /// <summary>
        /// Adds the specified stage operation.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(StageOperation item)
        {
            _items.Add(item);
            setStageData();
        }

        /// <summary>
        /// Adds the specified stage operations.
        /// Use this method if adding multiple operations to reduce calls to the API.
        /// </summary>
        /// <param name="items">The items.</param>
        public void Add(List<StageOperation> items)
        {
            _items.AddRange(items);
            setStageData();
        }

        /// <summary>
        /// Removes the specified stage operation by index.
        /// </summary>
        /// <param name="index">The index to remove.</param>
        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
            setStageData();
        }

        /// <summary>
        /// Removes all operations from the stage.
        /// </summary>
        public void Clear()
        {
            _items.Clear();
            setStageData();
        }
        #endregion

        #region API Methods
        /// <summary>
        /// Sets the stage data for the specified stage in the specified load case. <para />
        /// All previous stage data for the specified stage is cleared when this function is called.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setStageData()
        {
            _apiStaticNonlinearStaged?.SetStageData(CaseName, StageNumber,
                _items.Select(o => o.Operation).ToArray(),
                _items.Select(o => o.ObjectType).ToArray(),
                _items.Select(o => o.NameObject).ToArray(),
                _items.Select(o => o.Age).ToArray(),
                _items.Select(o => o.LoadOrObjectType).ToArray(),
                _items.Select(o => o.LoadOrObjectName).ToArray(),
                _items.Select(o => o.ScaleFactor).ToArray());
        }
        #endregion
        
    }
}
