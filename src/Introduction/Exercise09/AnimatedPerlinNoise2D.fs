//! Exercise I.9: Animated 2D Perlin noise
module NatureOfCode.Introduction.Exercise09.AnimatedPerlinNoise2D

open P5.Core
open P5.Rendering
open P5.Math
open P5.Color
open P5.Image

let width = 720
let height = 200

let setup p5 = createCanvas p5 width height

let draw p5 t =
    loadPixels p5

    let zOff = (float t / 10000.0)

    for x in { 0 .. width - 1 } do
        for y in { 0 .. height - 1 } do
            let xOff = float x * 0.01
            let yOff = float y * 0.01

            let intensity = map p5 (noise3D p5 xOff yOff zOff) 0 1 0 255
            setPixel p5 x y (Grayscale intensity)

    updatePixels p5

let run node = animate node setup draw
