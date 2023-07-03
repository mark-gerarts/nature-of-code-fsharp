//! Example I.4: Gaussian distribution
module NatureOfCode.Introduction.Example04.GaussianDistribution

open P5.Core
open P5.Rendering
open P5.Math
open P5.Color
open P5.Shape

let setup p5 = createCanvas p5 720 400

let draw p5 _ =
    let randomNum = randomGaussian p5
    let sd = 60.0
    let mean = 360.0

    let x = sd * randomNum + mean

    noStroke p5
    fill p5 (GrayscaleA(0, 10))
    circle p5 x 200 16


let run node = animate node setup draw
