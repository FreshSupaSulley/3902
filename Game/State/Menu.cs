using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game.Util;
using Myra;
using Myra.Graphics2D.UI;

namespace Game.State
{
    public class Menu : IGameState
    {
        private GraphicsDevice device;
        private Desktop _desktop;

        public Menu(GraphicsDevice device)
        {
            this.device = device;

            MyraEnvironment.Game = Main.INSTANCE;

            var grid = new Grid
            {
                RowSpacing = 8,
                ColumnSpacing = 8
            };

            grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));

            var helloWorld = new Label
            {
                Id = "label",
                Text = "Hello, World!"
            };
            grid.Widgets.Add(helloWorld);

            // ComboBox
            var combo = new ComboView();
            Grid.SetColumn(combo, 1);
            Grid.SetRow(combo, 0);

            combo.Widgets.Add(new Label { Text = "Red", TextColor = Color.Red });
            combo.Widgets.Add(new Label { Text = "Green", TextColor = Color.Green });
            combo.Widgets.Add(new Label { Text = "Blue", TextColor = Color.Blue });

            grid.Widgets.Add(combo);

            // Button
            var button = new Button
            {
                Content = new Label
                {
                    Text = "Show"
                }
            };
            Grid.SetColumn(button, 0);
            Grid.SetRow(button, 1);

            button.Click += (s, a) =>
            {
                var messageBox = Dialog.CreateMessageBox("Message", "Some message!");
                messageBox.ShowModal(_desktop);
            };

            grid.Widgets.Add(button);

            // Spin button
            var spinButton = new SpinButton
            {
                Width = 100,
                Nullable = true
            };
            Grid.SetColumn(spinButton, 1);
            Grid.SetRow(spinButton, 1);

            grid.Widgets.Add(spinButton);

            // Add it to the desktop
            _desktop = new Desktop();
            _desktop.Root = grid;
        }

        public void Update(GameTime gameTime)
        {
            if (Main.INSTANCE.mouse.LeftDown())
            {
                // Main.SwitchGameState(new Game(device));
            }
        }

        public void Draw(SpriteBatch batch)
        {
            _desktop.Render();
            // batch.Begin();
            // FontRenderer.Text("crude home menu for functionality check (left click the screen)!!!!", batch, new(100, 100));
            // batch.End();
        }
    }
}
