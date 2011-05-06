using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AnoleOlympics
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<GameObject> objects = new List<GameObject>();

        Vector3 cameraPosition = new Vector3(0f, 0.0f, 0.0f);
        Matrix cameraProjectionMatrix;
        Matrix cameraViewMatrix;
        
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            UpdateView();
            GameObject lizard = new GameObject();
            lizard.Name = "Lizard";
            lizard.Model = Content.Load<Model>("Model\\Lizard");
            lizard.Position = new Vector3(0f, 0f, -50f);
            lizard.Rotation = new Vector3((float)(Math.PI / -2), 0f, 0f);
            objects.Add(lizard);


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        protected void UpdateView()
        {
            Vector3 lookAt = new Vector3(0f, 0f, -1f);
            cameraViewMatrix = Matrix.CreateLookAt(cameraPosition, lookAt, Vector3.Up);
            cameraProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45.0f), graphics.GraphicsDevice.Viewport.AspectRatio, 1.0f, 1000.0f);
        }
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            objects[0].RotateY(0.01f);
            objects[0].RotateZ(0.01f);
            objects[0].RotateX(0.01f);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach(GameObject lizard in objects)
            {
                lizard.Draw(cameraViewMatrix, cameraProjectionMatrix);
            }

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
