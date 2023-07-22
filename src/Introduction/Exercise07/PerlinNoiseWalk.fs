//! Exercise I.7: Random walk with Perlin noise step size
module NatureOfCode.Introduction.Exercise07.PerlinNoiseWalk

open P5.Core
open P5.Environment
open P5.Color
open P5.Shape
open P5.Rendering
open P5.Math

type Pos = { x: float; y: float }

type State =
    { lastPos: Pos
      currentPos: Pos
      tx: float
      ty: float }

let random = System.Random()

let setup p5 =
    createCanvas p5 720 400
    background p5 (Grayscale 255)
    stroke p5 (Grayscale 0)

    let centerX = width p5 / 2 |> float
    let centerY = height p5 / 2 |> float
    let center = { x = centerX; y = centerY }

    { lastPos = center
      currentPos = center
      tx = random.NextDouble() * 1000.0
      ty = random.NextDouble() * 1000.0 }

let update p5 state =
    let xStep = map p5 (noise p5 state.tx) 0 1 -1 1
    let yStep = map p5 (noise p5 state.ty) 0 1 -1 1

    let newPos =
        { x = state.currentPos.x + xStep
          y = state.currentPos.y + yStep }

    { lastPos = state.currentPos
      currentPos = newPos
      tx = state.tx + 0.01
      ty = state.ty + 0.01 }

let draw p5 state =
    let p1 = state.lastPos
    let p2 = state.currentPos
    line p5 p1.x p1.y p2.x p2.y

let run node = simulate node setup update draw
