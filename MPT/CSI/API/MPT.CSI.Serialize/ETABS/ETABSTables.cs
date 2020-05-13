using System.Collections.Generic;

namespace MPT.CSI.Serialize.ETABS
{
    internal class ETABSTables : ProgramTables
    {
        public const string ANALYSIS_OPTIONS = "ANALYSIS OPTIONS";
        public const string AUTO_SELECT_SECTION_LISTS = "AUTO SELECT SECTION LISTS";
        public const string COMPOSITE_DESIGN_PREFERENCES = "COMPOSITE DESIGN PREFERENCES";
        public const string CONCRETE_DESIGN_OVERWRITES = "CONCRETE DESIGN OVERWRITES";
        public const string CONCRETE_DESIGN_PREFERENCES = "CONCRETE DESIGN PREFERENCES";
        public const string CONCRETE_SECTIONS = "CONCRETE SECTIONS";
        public const string CONCRETE_SLAB_DESIGN_PREFERENCES = "CONCRETE SLAB DESIGN PREFERENCES";
        public const string CONTROLS = "CONTROLS";
        public const string DECK_PROPERTIES = "DECK PROPERTIES";
        public const string DIAPHRAGM_NAMES = "DIAPHRAGM NAMES";
        public const string DIMENSION_LINES = "DIMENSION LINES";
        public const string FRAME_OBJECT_LOADS = "FRAME OBJECT LOADS";
        public const string FRAME_SECTIONS = "FRAME SECTIONS";
        public const string FUNCTIONS = "FUNCTIONS";
        public const string GENERALIZED_DISPLACEMENTS = "GENERALIZED DISPLACEMENTS";
        public const string GRIDS = "GRIDS";
        public const string LINE_ASSIGNS = "LINE ASSIGNS";
        public const string LINE_CONNECTIVITIES = "LINE CONNECTIVITIES";
        public const string LINK_PROPERTIES = "LINK PROPERTIES";
        public const string LOAD_CASES = "LOAD CASES";
        public const string LOAD_COMBINATIONS = "LOAD COMBINATIONS";
        public const string LOAD_PATTERNS = "LOAD PATTERNS";
        public const string LOG = "LOG";
        public const string MASS_SOURCE = "MASS SOURCE";
        public const string MATERIAL_PROPERTIES = "MATERIAL PROPERTIES";
        public const string PANEL_ZONE_PROPERTIES = "PANEL ZONE PROPERTIES";
        public const string PIER_SPANDREL_NAMES = "PIER/SPANDREL NAMES";
        public const string POINT_ASSIGNS = "POINT ASSIGNS";
        public const string POINT_COORDINATES = "POINT COORDINATES";
        public const string POINT_OBJECT_LOADS = "POINT OBJECT LOADS";
        public const string PROGRAM_INFORMATION = "PROGRAM INFORMATION";
        public const string PROJECT_INFORMATION = "PROJECT INFORMATION";
        public const string REBAR_DEFINITIONS = "REBAR DEFINITIONS";
        public const string SLAB_PROPERTIES = "SLAB PROPERTIES";
        public const string STEEL_DESIGN_PREFERENCES = "STEEL DESIGN PREFERENCES";
        public const string STORIES_IN_SEQUENCE_FROM_TOP = "STORIES - IN SEQUENCE FROM TOP";
        public const string TABLE_SETS = "TABLE SETS";
        public const string TENDON_SECTIONS = "TENDON SECTIONS";
        public const string WALL_DESIGN_PREFERENCES = "WALL DESIGN PREFERENCES";
        public const string WALL_PROPERTIES = "WALL PROPERTIES";


        public ETABSTables()
        {
            TableNames = new List<string>()
            {
                ANALYSIS_OPTIONS,
                AUTO_SELECT_SECTION_LISTS,
                COMPOSITE_DESIGN_PREFERENCES,
                CONCRETE_DESIGN_OVERWRITES,
                CONCRETE_DESIGN_PREFERENCES,
                CONCRETE_SECTIONS,
                CONCRETE_SLAB_DESIGN_PREFERENCES,
                CONTROLS,
                DECK_PROPERTIES,
                DIAPHRAGM_NAMES,
                DIMENSION_LINES,
                FRAME_OBJECT_LOADS,
                FRAME_SECTIONS,
                FUNCTIONS,
                GENERALIZED_DISPLACEMENTS,
                GRIDS,
                LINE_ASSIGNS,
                LINE_CONNECTIVITIES,
                LINK_PROPERTIES,
                LOAD_CASES,
                LOAD_COMBINATIONS,
                LOAD_PATTERNS,
                LOG,
                MASS_SOURCE,
                MATERIAL_PROPERTIES,
                PANEL_ZONE_PROPERTIES,
                PIER_SPANDREL_NAMES,
                POINT_ASSIGNS,
                POINT_COORDINATES,
                POINT_OBJECT_LOADS,
                PROGRAM_INFORMATION,
                PROJECT_INFORMATION,
                REBAR_DEFINITIONS,
                SLAB_PROPERTIES,
                STEEL_DESIGN_PREFERENCES,
                STORIES_IN_SEQUENCE_FROM_TOP,
                TABLE_SETS,
                TENDON_SECTIONS,
                WALL_DESIGN_PREFERENCES,
                WALL_PROPERTIES,

            };
        }
    }
}
