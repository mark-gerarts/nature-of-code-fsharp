//! Example I.4: Gaussian paint splatter
module NatureOfCode.Introduction.Exercise04.GaussianPaintSplatter

open P5.Core
open P5.Rendering
open P5.Math
open P5.Color
open P5.Shape

type Dot = { x: float; y: float; size: float }

let baseSize = 50
let numSplatters = 70
let width = 720
let height = 400

let randomColor p5 =
    let r = randomMax p5 255
    let g = randomMax p5 255
    let b = randomMax p5 255
    let a = 10

    RGBA(r, g, b, a)

let newDot p5 =
    let x = 60.0 * (randomGaussian p5) + (width / 2 |> float)
    let y = 60.0 * (randomGaussian p5) + (height / 2 |> float)
    let size = 10.0 * (randomGaussian p5) + (float baseSize)

    { x = x; y = y; size = size }


let setup p5 =
    createCanvas p5 width height

    noStroke p5
    fill p5 (randomColor p5)

    []


let update p5 dots =
    if List.length dots >= numSplatters then
        dots
    else
        newDot p5 :: dots

let draw p5 dots =

    for dot in dots do
        circle p5 dot.x dot.y baseSize


let run node = simulate node setup update draw
