#r "nuget: Legivel"

open Legivel.Serialization
open System.IO

type Sketch =
    { name: string
      file: string
      index: string }

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

let getFilenameForSketch (chapter: Chapter) (sketch: Sketch) =
    sprintf "%s/%s.html" chapter.directory sketch.file

let generateHomepage (siteStructure: Site) =
    let mutable toc = [ "<ul>" ]

    // Quick & dirty procedural.
    for chapter in siteStructure.content do
        let chapterUrl = sprintf "./%s" chapter.directory
        toc <- List.append toc [ sprintf "<li><a href=\"%s\">%s</a><ul>" chapterUrl chapter.name ]

        for sketch in chapter.content do
            let sketchUrl = getFilenameForSketch chapter sketch

            toc <- List.append toc [ sprintf "<li><a href=\"%s\">%s</a></li>" sketchUrl sketch.name ]

        toc <- List.append toc [ "</ul></li>" ]

    toc <- List.append toc [ "</ul>" ]

    File.ReadAllText "./gh-pages/index.html"
    |> fun p -> p.Replace("{{ title }}", siteStructure.title)
    |> fun p -> p.Replace("{{ subtitle }}", "")
    |> fun p -> p.Replace("{{ introduction }}", siteStructure.introduction)
    |> fun p -> p.Replace("{{ content }}", String.concat "" toc)
    |> fun p -> p.Replace("{{ stylesheet }}", "./style.css")
    |> fun p -> File.WriteAllText("./gh-pages/output/index.html", p)

let generateChapterIndex (siteStructure: Site) (chapter: Chapter) =
    let mutable toc = [ "<ul>" ]

    for sketch in chapter.content do
        let sketchUrl = sprintf "./%s.html" sketch.file

        toc <- List.append toc [ sprintf "<li><a href=\"%s\">%s</a></li>" sketchUrl sketch.name ]

    toc <- List.append toc [ "</ul>" ]

    Directory.CreateDirectory <| sprintf "./gh-pages/output/%s" chapter.directory
    |> ignore

    File.ReadAllText "./gh-pages/index.html"
    |> fun p -> p.Replace("{{ title }}", siteStructure.title)
    |> fun p -> p.Replace("{{ subtitle }}", chapter.name)
    |> fun p -> p.Replace("{{ introduction }}", "<a href=\"../\">« Home</a>")
    |> fun p -> p.Replace("{{ content }}", String.concat "" toc)
    |> fun p -> p.Replace("{{ stylesheet }}", "./../style.css")
    |> fun p -> File.WriteAllText(sprintf "./gh-pages/output/%s/index.html" chapter.directory, p)

let generateSketch (siteStructure: Site) (chapter: Chapter) (sketch: Sketch) =
    let filename = getFilenameForSketch chapter sketch

    File.ReadAllText "./gh-pages/sketch.html"
    |> fun p -> p.Replace("{{ siteTitle }}", siteStructure.title)
    |> fun p -> p.Replace("{{ chapter }}", chapter.name)
    |> fun p -> p.Replace("{{ sketchTitle }}", sketch.name)
    |> fun p -> p.Replace("{{ sketchName }}", sprintf "%s/%s" chapter.directory sketch.file)
    |> fun p -> p.Replace("{{ sketchPath }}", sprintf "%s/%s/%s.fs" chapter.directory sketch.index sketch.file)
    |> fun p -> File.WriteAllText(sprintf "./gh-pages/output/%s" filename, p)

let generateSketches (siteStructure: Site) =
    for chapter in siteStructure.content do
        generateChapterIndex siteStructure chapter

        for sketch in chapter.content do
            generateSketch siteStructure chapter sketch

match parseResult with
| [ Success { Data = siteStructure } ] ->
    generateHomepage siteStructure
    generateSketches siteStructure
| _ -> printf "%A" parseResult
