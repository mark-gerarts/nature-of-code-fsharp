module NatureOfCode.Introduction.Example03.RightWalker

open P5.Core
open P5.Environment
open P5.Color
open P5.Shape
open P5.Rendering

type Walker = { x: int; y: int }

let random = new System.Random()

let setup p5 =
    createCanvas p5 720 400
    background p5 (Grayscale 255)
    stroke p5 (Grayscale 0)

    { x = width p5 / 2; y = height p5 / 2 }

let update _ walker =
    let r = random.NextDouble()

    match r with
    | r when r < 0.4 -> { walker with x = walker.x + 1 }
    | r when r < 0.6 -> { walker with x = walker.x - 1 }
    | r when r < 0.8 -> { walker with y = walker.y + 1 }
    | _ -> { walker with y = walker.y - 1 }

let draw p5 walker =
    // Adding .5 yields better results with regards to aliasing.
    point p5 (float walker.x + 0.5) (float walker.y + 0.5)

let run node = simulate node setup update draw
