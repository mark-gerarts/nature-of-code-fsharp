module NatureOfCode.App

open P5.Core
open NatureOfCode

let private sketches =
    [ "Introduction/Example01/TraditionalRandomWalk", Introduction.Example01.TraditionalRandomWalk.run
      "Introduction/Example02/RandomNumberDistribution", Introduction.Example02.RandomNumberDistribution.run
      "Introduction/Example03/RightWalker", Introduction.Example03.RightWalker.run
      "Introduction/Example04/GaussianDistribution", Introduction.Example04.GaussianDistribution.run
      "Introduction/Example05/PerlinNoiseWalker", Introduction.Example05.PerlinNoiseWalker.run
      "Introduction/Example06/PerlinNoise2D", Introduction.Example06.PerlinNoise2D.run
      "Introduction/Exercise01/DownRightWalker", Introduction.Exercise01.DownRightWalker.run
      "Introduction/Exercise03/DynamicRandomWalk", Introduction.Exercise03.DynamicRandomWalk.run
      "Introduction/Exercise04/GaussianPaintSplatter", Introduction.Exercise04.GaussianPaintSplatter.run
      "Introduction/Exercise05/GaussianRandomWalk", Introduction.Exercise05.GaussianRandomWalk.run
      "Introduction/Exercise06/CustomDistributionRandomWalk", Introduction.Exercise06.CustomDistributionRandomWalk.run
      "Introduction/Exercise07/PerlinNoiseWalk", Introduction.Exercise07.PerlinNoiseWalk.run
      "Introduction/Exercise08/AdjustablePerlinNoise2D", Introduction.Exercise08.AdjustablePerlinNoise2D.run
      "Introduction/Exercise09/AnimatedPerlinNoise2D", Introduction.Exercise09.AnimatedPerlinNoise2D.run
      "Introduction/Exercise10/PerlinLandscape", Introduction.Exercise10.PerlinLandscape.run
      "Vectors/Example01/BouncingBallWithNoVectors", Vectors.Example01.BouncingBallWithNoVectors.run
      "Vectors/Example02/BouncingBallWithPVectors", Vectors.Example02.BouncingBallWithPVectors.run
      "Vectors/Example03/VectorSubtraction", Vectors.Example03.VectorSubtraction.run
      "Vectors/Example04/VectorMultiplication", Vectors.Example04.VectorMultiplication.run
      "Vectors/Example05/VectorMagnitude", Vectors.Example05.VectorMagnitude.run
      "Vectors/Example06/VectorNormalization", Vectors.Example06.VectorNormalization.run
      "Vectors/Example07/Motion101Velocity", Vectors.Example07.Motion101Velocity.run
      "Vectors/Example08/Motion101ConstantAcceleration", Vectors.Example08.Motion101ConstantAcceleration.run
      "Vectors/Exercise02/VectorWalk", Vectors.Exercise02.VectorWalk.run
      "Vectors/Exercise03/BouncingBall3D", Vectors.Exercise03.BouncingBall3D.run ]
    |> Map.ofList

let runSketch name canvasSelector =
    let node = Element <| Browser.Dom.document.querySelector (canvasSelector)

    if not <| sketches.ContainsKey name then
        failwith <| sprintf "No sketch with name %s exists" name

    sketches[name]node

let allNames = sketches |> Map.toSeq |> Seq.map fst |> Seq.toArray
