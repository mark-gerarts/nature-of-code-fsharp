//! Example I.2: Random number distribution
module NatureOfCode.Introduction.Example02.RandomNumberDistribution

open P5.Core
open P5.Rendering
open P5.Color
open P5.Shape

let random = new System.Random()
let width = 720
let height = 400


let setup p5 =
    createCanvas p5 width height
    Array.zeroCreate 20

let update _ (randomCounts: int array) =
    let randomIndex = random.Next(20)
    randomCounts[randomIndex] <- randomCounts[randomIndex] + 1

    randomCounts

let draw p5 (randomCounts: int array) =
    background p5 (Grayscale 255)
    stroke p5 (Grayscale 0)
    fill p5 (Grayscale 175)

    let w = width / Array.length randomCounts

    randomCounts
    |> Array.iteri (fun x n -> rect p5 (x * w |> float) (height - n |> float) (w - 1 |> float) n)

let run node = simulate node setup update draw
