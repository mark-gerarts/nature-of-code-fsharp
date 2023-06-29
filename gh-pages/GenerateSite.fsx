#r "nuget: Legivel"

open Legivel.Serialization
open System.IO

type Sketch = { name: string; file: string }

type Chapter =
    { name: string
      directory: string
      content: Sketch list }

type Site =
    { title: string
      introduction: string
      content: Chapter list }

let yaml = File.ReadAllText "./gh-pages/site-structure.yml"
let parseResult = Deserialize<Site> yaml

let generateHomepage (siteStructure: Site) =
    let homepage =
        File.ReadAllText "./gh-pages/index.html"
        |> fun p -> p.Replace("{{ title }}", siteStructure.title)
        |> fun p -> p.Replace("{{ introduction }}", siteStructure.introduction)

    homepage

match parseResult with
| [ Success { Data = siteStructure } ] -> printf "%A" (generateHomepage siteStructure)
| _ -> printf "%A" parseResult
