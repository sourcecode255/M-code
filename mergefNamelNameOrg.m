//merge first_name, last_name, org

let
    Source = (first_name as text, last_name as text, org as text) => 
let 
    step1 = Table.TransformColumnTypes({{"first_name", type text}, {"last_name", type text}, {"org", type text}}),
    step2 = Text.Combine ({first_name, last_name, Text.Combine({"(", org, ")"})}," ") as text

in step2
in
    Source
