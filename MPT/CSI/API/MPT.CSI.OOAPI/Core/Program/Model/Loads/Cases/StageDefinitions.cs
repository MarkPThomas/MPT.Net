// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-06-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-06-2018
// ***********************************************************************
// <copyright file="StageDefinitions.cs" company="">
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
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// A set of stage definitions with associated data.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class StageDefinitions : CSiOoApiBaseBase
    {
        #region Fields & Properties
        /// <summary>
        /// The static nonlinear staged data object from the API.
        /// </summary>
        protected IStaticNonlinearStaged _apiStaticNonlinearStaged;

        /// <summary>
        /// List of stage definition items.
        /// </summary>
        private List<StageDefinition> _items = new List<StageDefinition>();

        /// <summary>
        /// List of stage definition items
        /// </summary>
        /// <value>The items.</value>
        public ReadOnlyCollection<StageDefinition> Items => new ReadOnlyCollection<StageDefinition>(_items);

        /// <summary>
        /// The name of the load case.
        /// </summary>
        /// <value>The name of the case.</value>
        public string CaseName { get; protected set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="StageDefinitions" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="appStaticNonlinearStaged">The application static nonlinear staged.</param>
        /// <param name="caseName">Name of the case.</param>
        public StageDefinitions(ApiCSiApplication app, IStaticNonlinearStaged appStaticNonlinearStaged, string caseName)
            : base(app)
        {
            _apiStaticNonlinearStaged = appStaticNonlinearStaged;
            CaseName = caseName;
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public void FillData()
        {
            FillStageDefinitions();
            FillStageOperations();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the load case name associated with the stages.
        /// </summary>
        /// <param name="name">The load case name.</param>
        public void SetCaseName(string name)
        {
            CaseName = name;
            foreach (var item in _items)
            {
                item.SetCaseName(name);
            }
        }
        #endregion

        #region Stage Definitions
        /// <summary>
        /// Returns the stage definition data for the load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillStageDefinitions()
        {
            if (_apiStaticNonlinearStaged == null) return;

            _apiStaticNonlinearStaged.GetStageDefinitions(CaseName,
                out var duration,
                out var outputIsToBeSaved,
                out var nameOutput,
                out var comment);

            _items = new List<StageDefinition>();
            for (int i = 0; i < duration.Length; i++)
            {
                StageDefinition definition = new StageDefinition(_apiApp)
                {
                    Duration = duration[i],
                    OutputIsToBeSaved = outputIsToBeSaved[i],
                    NameOutput = nameOutput[i],
                    Comment = comment[i]
                };
                _items.Add(definition);
            }
        }

        /// <summary>
        /// This function initializes the stage definition data for the load case. <para />
        /// All previous stage definition data for the case is cleared when this function is called.
        /// </summary>
        /// <param name="definitions">The definitions.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetStageDefinitions(List<StageDefinition> definitions)
        {
            _items = definitions;
            setStageDefinitions();
        }


        /// <summary>
        /// Gets the stage by stage number.
        /// </summary>
        /// <param name="stageNumber">The stage number to return.</param>
        /// <returns>StageDefinition.</returns>
        public StageDefinition GetStage(int stageNumber)
        {
            return _items[stageNumber - 1];
        }

        /// <summary>
        /// Gets the stage by index.
        /// </summary>
        /// <param name="index">The index to return.</param>
        /// <returns>StageDefinition.</returns>
        public StageDefinition GetStageAt(int index)
        {
            return _items[index];
        }

        /// <summary>
        /// Adds the specified stage definition.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(StageDefinition item)
        {
            _items.Add(item);
            setStageDefinitions();
        }

        /// <summary>
        /// Adds the specified stage definitions.
        /// Use this method if adding multiple operations to reduce calls to the API.
        /// </summary>
        /// <param name="items">The items.</param>
        public void Add(List<StageDefinition> items)
        {
            _items.AddRange(items);
            setStageDefinitions();
        }

        /// <summary>
        /// Removes the specified stage by index.
        /// </summary>
        /// <param name="index">The indexto remove.</param>
        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
            setStageDefinitions();
        }

        /// <summary>
        /// Removes the specified stage by number.
        /// </summary>
        /// <param name="stageNumber">The stage number to remove.</param>
        public void RemoveStage(int stageNumber)
        {
            _items.RemoveAt(stageNumber - 1);
            setStageDefinitions();
        }

        /// <summary>
        /// Removes all stage definitions.
        /// </summary>
        public void Clear()
        {
            _items.Clear();
            setStageDefinitions();
        }
        #endregion

        #region Stage Operations
        /// <summary>
        /// Returns stage operations for the specified stage in the load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillStageOperations()
        {
            int stage = 0;
            foreach (var item in _items)
            {
                stage++;
                item.FillStageOperations(CaseName, stage);
            }
        }


        /// <summary>
        /// Adds the specified stage operation.
        /// </summary>
        /// <param name="stageNumber">The stage number to add the operation to.</param>
        /// <param name="item">The item.</param>
        public void AddStageOperation(int stageNumber, StageOperation item)
        {
            AddStageOperationAt(stageNumber - 1, item);
        }

        /// <summary>
        /// Adds the specified stage operation.
        /// </summary>
        /// <param name="stageIndex">The stage index to add the operation to.</param>
        /// <param name="item">The item.</param>
        public void AddStageOperationAt(int stageIndex, StageOperation item)
        {
            _items[stageIndex].AddStageOperation(item);
        }


        /// <summary>
        /// Adds the specified stage operations.
        /// Use this method if adding multiple operations to reduce calls to the API.
        /// </summary>
        /// <param name="stageNumber">The stage number to add the operations to.</param>
        /// <param name="items">The items.</param>
        public void AddStageOperations(int stageNumber, List<StageOperation> items)
        {
            AddStageOperationsAt(stageNumber - 1, items);
        }

        /// <summary>
        /// Adds the specified stage operations.
        /// Use this method if adding multiple operations to reduce calls to the API.
        /// </summary>
        /// <param name="stageIndex">The stage index to add the operations to.</param>
        /// <param name="items">The items.</param>
        public void AddStageOperationsAt(int stageIndex, List<StageOperation> items)
        {
            _items[stageIndex].AddStageOperations(items);
        }


        /// <summary>
        /// Removes the specified stage operation by stage number.
        /// </summary>
        /// <param name="stageNumber">The stage number to remove the operation from.</param>
        /// <param name="operationIndex">The operation index to remove.</param>
        public void RemoveStageOperation(int stageNumber, int operationIndex)
        {
            RemoveStageOperationAt(stageNumber - 1, operationIndex);
        }

        /// <summary>
        /// Removes the specified stage operation by index.
        /// </summary>
        /// <param name="stageIndex">The stage index to remove the operation from.</param>
        /// <param name="operationIndex">The operation index to remove.</param>
        public void RemoveStageOperationAt(int stageIndex, int operationIndex)
        {
            _items[stageIndex].RemoveStageOperation(operationIndex);
            updateStageNumbers();
        }


        /// <summary>
        /// Removes all operations from the stage.
        /// </summary>
        /// <param name="stageNumber">The stage number to clear the operations from.</param>
        public void ClearStageOperations(int stageNumber)
        {
            ClearStageOperationsAt(stageNumber - 1);
        }

        /// <summary>
        /// Removes all operations from the stage.
        /// </summary>
        /// <param name="stageIndex">The stage index to clear the operations from.</param>
        public void ClearStageOperationsAt(int stageIndex)
        {
            _items[stageIndex].ClearStageOperations();
            updateStageNumbers();
        }
        #endregion
        
        #region Protected       
        /// <summary>
        /// Updates the stage numbers.
        /// </summary>
        protected void updateStageNumbers()
        {
            int stage = 0;
            foreach (var item in _items)
            {
                stage++;
                item.SetStageNumber(stage);
            }
        }
        #endregion

        #region API Methods   
        /// <summary>
        /// This function initializes the stage definition data for the load case. <para />
        /// All previous stage definition data for the case is cleared when this function is called.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setStageDefinitions()
        {
            _apiStaticNonlinearStaged?.SetStageDefinitions(CaseName,
                _items.Select(o => o.Duration).ToArray(),
                _items.Select(o => o.OutputIsToBeSaved).ToArray(),
                _items.Select(o => o.NameOutput).ToArray(),
                _items.Select(o => o.Comment).ToArray());
        }
        #endregion
    }
}
