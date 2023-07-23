open System
open System.IO
open System.Text.RegularExpressions

type Sketch =
    { name: string
      file: string
      directory: string }

type Chapter =
    { name: string
      directory: string
      sketches: Sketch seq }

type Site =
    { subtitle: string
      chapters: Chapter seq }

(*
    Generating structure from directory tree.
*)

let filename (path: string) = path.Split "/" |> Seq.last

let getNameFromSketchFile fileContents =
    let regex = Regex(@"\/\/! (.*)", RegexOptions.Compiled)
    let matches = regex.Match(fileContents)

    matches.Groups[1].Value

let generateSketchStructure sketchDir =
    let files = Directory.EnumerateFiles sketchDir

    let mainSketchPath, mainSketchFile =
        files
        |> Seq.map (fun path -> path, File.ReadAllText path)
        |> Seq.find (fun (_, text) -> text.Contains "//! ")

    let directory = (mainSketchPath.Split "/")[3]

    let filename = filename mainSketchPath

    { name = getNameFromSketchFile mainSketchFile
      file = filename.Substring(0, filename.Length - 3)
      directory = directory }

let generateChapterStructure chapterDir =
    let directory = filename chapterDir
    let name = File.ReadAllText(chapterDir + "/name.txt")

    let sketches =
        Directory.EnumerateDirectories chapterDir
        |> Seq.sort
        |> Seq.map generateSketchStructure

    { name = name
      directory = directory
      sketches = sketches }

let siteStructure =
    let subtitle = File.ReadAllText "./src/index.html"

    let chapters =
        Directory.EnumerateDirectories "./src/"
        |> Seq.filter (fun dir -> Char.IsUpper(dir.Chars(6)))
        |> Seq.map generateChapterStructure

    { subtitle = subtitle
      chapters = chapters }

(*
    Generating static site based on structure.
*)

let getFilenameForSketch (chapter: Chapter) (sketch: Sketch) =
    sprintf "%s/%s.html" chapter.directory sketch.file

let generateHomepage (siteStructure: Site) =
    let mutable toc = [ "<ul>" ]

    // Quick & dirty procedural.
    for chapter in siteStructure.chapters do
        let chapterUrl = sprintf "./%s" chapter.directory
        toc <- List.append toc [ sprintf "<li><a href=\"%s\">%s</a><ul>" chapterUrl chapter.name ]

        for sketch in chapter.sketches do
            let sketchUrl = getFilenameForSketch chapter sketch

            toc <- List.append toc [ sprintf "<li><a href=\"%s\">%s</a></li>" sketchUrl sketch.name ]

        toc <- List.append toc [ "</ul></li>" ]

    toc <- List.append toc [ "</ul>" ]

    File.ReadAllText "./gh-pages/index.html"
    |> fun p -> p.Replace("{{ subtitle }}", siteStructure.subtitle)
    |> fun p -> p.Replace("{{ content }}", String.concat "" toc)
    |> fun p -> p.Replace("{{ stylesheet }}", "./style.css")
    |> fun p -> File.WriteAllText("./gh-pages/output/index.html", p)

let generateChapterIndex (siteStructure: Site) (chapter: Chapter) =
    let mutable toc = [ "<a href=\"../\">« Home</a>"; "<ul>" ]

    for sketch in chapter.sketches do
        let sketchUrl = sprintf "./%s.html" sketch.file

        toc <- List.append toc [ sprintf "<li><a href=\"%s\">%s</a></li>" sketchUrl sketch.name ]

    toc <- List.append toc [ "</ul>" ]

    Directory.CreateDirectory <| sprintf "./gh-pages/output/%s" chapter.directory
    |> ignore

    File.ReadAllText "./gh-pages/index.html"
    |> fun p -> p.Replace("{{ subtitle }}", sprintf "<h2>%s</h2>" chapter.name)
    |> fun p -> p.Replace("{{ content }}", String.concat "" toc)
    |> fun p -> p.Replace("{{ stylesheet }}", "./../style.css")
    |> fun p -> File.WriteAllText(sprintf "./gh-pages/output/%s/index.html" chapter.directory, p)

let generateSketch (chapter: Chapter) (sketch: Sketch) =
    let filename = getFilenameForSketch chapter sketch

    File.ReadAllText "./gh-pages/sketch.html"
    |> fun p -> p.Replace("{{ chapter }}", chapter.name)
    |> fun p -> p.Replace("{{ sketchTitle }}", sketch.name)
    |> fun p -> p.Replace("{{ sketchName }}", sprintf "%s/%s/%s" chapter.directory sketch.directory sketch.file)
    |> fun p -> p.Replace("{{ sketchPath }}", sprintf "%s/%s/%s.fs" chapter.directory sketch.directory sketch.file)
    |> fun p -> File.WriteAllText(sprintf "./gh-pages/output/%s" filename, p)

let generateSketches (siteStructure: Site) =
    for chapter in siteStructure.chapters do
        generateChapterIndex siteStructure chapter

        for sketch in chapter.sketches do
            generateSketch chapter sketch

generateHomepage siteStructure
generateSketches siteStructure
