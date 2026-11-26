(StartRef as number, 
//The last invoice number (LIN) for "ROOMS" invoices in Xero.
tbl as table, 
// A table with a column named [Contacts] and other columns matching column_names list
//SB2XI_3
column_names as table) 
// A single column table [Column1] with the names of all the required columns for the SelectColumns function below. Passed as a table because PQ will not axcept a list.
//SB2XI_3_col_names
=>   

/*
Power Query's Invoke Custom Function UI does not treat as list as "select a list-producing query".
Make the parameter a table instead of a list.
Then make your two queries return one-column tables, rather than lists.
*/

let
    //Columns = column_names[Column1], //This returns a list
    Columns = {"add-on","catering_product","po_ref","addon_product","Reference","Contact", "description", "catering_cost"},

// Ingest table with Contacts column only
    Source = tbl[Contact],
    Convert_to_table = Table.FromList(Source, Splitter.SplitByNothing(), null, null, ExtraValues.Error),
    Remove_Dups = Table.Distinct(Convert_to_table),
    Rename_Cols = Table.RenameColumns(Remove_Dups,{{"Column1", "Contact"}}),
    Index_Added = Table.AddIndexColumn(Rename_Cols, "Index", StartRef, 1, Int64.Type),

// Ingest table with columns from list
    SelectCols = Table.SelectColumns(tbl, Columns),
    
//Merge tables
    Merged_Queries = Table.NestedJoin(SelectCols, {"Contact"}, Index_Added, {"Contact"}, "Index", JoinKind.LeftOuter),
    Expand = Table.ExpandTableColumn(Merged_Queries, "Index", {"Index"}, {"Index"})
in
    Expand