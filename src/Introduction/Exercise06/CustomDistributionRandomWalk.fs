//! Exercise I.6: Custom distribution random walk
module NatureOfCode.Introduction.Exercise06.CustomDistributionRandomWalk

open P5.Core
open P5.Environment
open P5.Color
open P5.Shape
open P5.Rendering
open P5.Math

type Pos = { x: float; y: float }

type State = { lastPos: Pos; currentPos: Pos }

let random = System.Random()

let setup p5 =
    createCanvas p5 720 400
    background p5 (Grayscale 255)
    stroke p5 (Grayscale 0)

    let centerX = width p5 / 2 |> float
    let centerY = height p5 / 2 |> float
    let center = { x = centerX; y = centerY }

    { lastPos = center
      currentPos = center }


let rec customDistributionValue p5 =
    let v = random.NextDouble()
    let p = random.NextDouble()
    let sign = randomChoice p5 [ -1.0; 1.0 ]

    if v < p * p then
        v * 10.0 * sign
    else
        customDistributionValue p5

let update p5 state =
    let xStep = customDistributionValue p5
    let yStep = customDistributionValue p5

    let newPos =
        { x = state.currentPos.x + xStep
          y = state.currentPos.y + yStep }

    { lastPos = state.currentPos
      currentPos = newPos }

let draw p5 state =
    let p1 = state.lastPos
    let p2 = state.currentPos
    line p5 p1.x p1.y p2.x p2.y

let run node = simulate node setup update draw
