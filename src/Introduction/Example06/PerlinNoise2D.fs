//! Example I.6: 2D Perlin noise
module NatureOfCode.Introduction.Example06.PerlinNoise2D

open P5.Core
open P5.Rendering
open P5.Math
open P5.Color
open P5.Image

let draw p5 =
    let width = 720
    let height = 400
    createCanvas p5 width height

    loadPixels p5

    for x in { 0 .. width - 1 } do
        for y in { 0 .. height - 1 } do
            let xOff = float x * 0.01
            let yOff = float y * 0.01

            let intensity = map p5 (noise2D p5 xOff yOff) 0 1 0 255
            setPixel p5 x y (Grayscale intensity)

    updatePixels p5

let run node = display node draw
