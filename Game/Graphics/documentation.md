# Graphics Documentation

## IUserInterfaceLayout (interface)
- `void Draw(SpriteBatch sb)` draws the user interface into the SpriteBatch
- `void AddElement(IUserInterfaceElement el)` adds an element to the layout

## IUserInterfaceElement (interface)
- `void Draw(SpriteBatch sb)` draws the element into the SpriteBatch (likely only called by layout)
- `void Update(GameTime gameTime)`

## UIButton (class) : IUserInterfaceElement
- `constructor(Rectangle bounds, MouseController mc, ICommand onPress)`
- `void Draw(SpriteBatch sb)` renders a rectangular button

## UITextButton (class) : UIButton
- `constructor(Rectangle bounds, ICommand onPress, MouseController mc, String text SpriteFont font, Color color)`
- `void Draw(SpriteBatch sb)` renders the rectangular button and text

## UIText (class) : IUserInterfaceElement
- `constructor(String text, Vector2 pos)` take a string and position to render basic text
- `void Draw(SpriteBatch sb)` renders the text to the screen
- `public void Update(GameTime gameTime)` can be used to change the value in child classes

## UIVariableText<T> (class) : UIText
- `constructor(Func<T> getValue, Vector2 pos)` takes a reference to a variable to be stored and a position for the text
- `private void ConvertVariableToText()` give a means to add prefixes, suffixes, or change how the value is being interpreted.
- `public void Update(GameTime gameTime)` changes text based on updated variable value
