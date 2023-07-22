//! Example I.5: Perlin noise walker
module NatureOfCode.Introduction.Example05.PerlinNoiseWalker

open P5.Core
open P5.Rendering
open P5.Shape
open P5.Math
open P5.Environment
open P5.Color

type Walker =
    { x: float
      y: float
      tx: float
      ty: float }

let setup p5 =
    createCanvas p5 720 400

    { x = 360; y = 200; tx = 0; ty = 10000 }

let update p5 walker =
    let width = width p5 |> float
    let height = height p5 |> float
    let x = map p5 (noise p5 walker.tx) 0 1 0 width
    let y = map p5 (noise p5 walker.ty) 0 1 0 height

    { x = x
      y = y
      tx = walker.tx + 0.01
      ty = walker.ty + 0.01 }

let draw p5 walker =
    background p5 (Grayscale 255)

    fill p5 (Grayscale 127)
    stroke p5 (Grayscale 0)
    strokeWeight p5 2

    circle p5 walker.x walker.y 48

let run node = simulate node setup update draw
