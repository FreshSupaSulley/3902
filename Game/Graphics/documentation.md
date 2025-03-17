# Graphics Documentation

## IUserInterfaceLayout (interface)
- `void Draw(SpriteBatch sb)` draws the user interface into the SpriteBatch
- `void AddElement(IUserInterfaceElement el)` adds an element to the layout

## IUserInterfaceElement (interface)
- `void Draw(SpriteBatch sb)` draws the element into the SpriteBatch (likely only called by layout)

## UIButton (class) : IUserInterfaceElement
- `constructor(Rectangle bounds, ICommand onPress)`
- `void Draw(SpriteBatch sb)`

## UITextButton (class) : UIButton
- `constructor(Rectangle bounds, ICommand onPress, String text)`
- `void Draw(SpriteBatch sb)`

## UIText (class) : IUserInterfaceElement
- `constructor(String text, Vector2 pos)`
- `void Draw(SpriteBatch sb)`

## UIVariableText<T> (class) : UIText
- `constructor(ref T text, Vector2 pos)`
- `private void ConvertVariableToText()`
- `void Draw(SpriteBatch sb)`
