//merge fName, lName, Org

let
    Source = (fName as text, lName as text, Org as text) => 
let 
    step1 = Table.TransformColumnTypes({{"fName", type text}, {"lName", type text}, {"Org", type text}}),
    step2 = Text.Combine ({fName, lName, Text.Combine({"(", Org, ")"})}," ") as text

in step2
in
    Source
