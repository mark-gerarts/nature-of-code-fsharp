//! Exercise I.4: Gaussian random walk
module NatureOfCode.Introduction.Exercise05.GaussianRandomWalk

open P5.Core
open P5.Environment
open P5.Color
open P5.Shape
open P5.Rendering
open P5.Math

type Pos = { x: float; y: float }

type State = { lastPos: Pos; currentPos: Pos }

let setup p5 =
    createCanvas p5 720 400
    background p5 (Grayscale 255)
    stroke p5 (Grayscale 0)

    let centerX = width p5 / 2 |> float
    let centerY = height p5 / 2 |> float
    let center = { x = centerX; y = centerY }

    { lastPos = center
      currentPos = center }

let update p5 state =
    let mean = 0.0
    let sd = 3.0

    let xStep = sd * (randomGaussian p5) + mean
    let yStep = sd * (randomGaussian p5) + mean

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
