﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Engine.Core;

//Author: Daniil Leshchenko
// 27.03.25
// Description: This class manages the core game engine logic,
// including input, camera, and active scenes. It handles loading, updating, and drawing scenes in the game.

namespace GameEngine.Engine.Managers
{
    public class GameEngine : DrawableGameComponent
    {
        private InputManager Input;
        public static CameraManager Cameras { get; set;}

        private static Scene activeScene;
        public static Scene ActiveScene {  get { return activeScene; } }

        public GameEngine(Game gameInstance): base(gameInstance)
        { 
            Game.Components.Add(this);

            Input = new InputManager(Game);
            Cameras = new CameraManager(Game);
        }

        public void LoadScene(Scene _scene)
        {
            if (_scene != null) 
            { 
                UnloadContent();
                activeScene = _scene;
                activeScene.Initialize();
            }
        }

        public void UnloadScene()
        {
            if (ActiveScene != null) 
            {
                activeScene = null;
                CameraManager.Clear();
                Utilities.Content.Unload();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if(ActiveScene != null) 
            {
                activeScene.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if(activeScene != null && CameraManager.ActiveCamera != null) 
            {
                activeScene.Draw(CameraManager.ActiveCamera);
            }

            base.Draw(gameTime);
        }
    }
}
