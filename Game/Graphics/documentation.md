# Graphics Documentation

## IUserInterfaceLayout (interface)
- `void Draw(SpriteBatch)` draws the user interface into the SpriteBatch
- `void AddElement(IUserInterfaceElement)` adds an element to the layout

## IUserInterfaceElement (interface)
- `void Draw(SpriteBatch)` draws the element into the SpriteBatch (likely only called by layout)

## UIButton (class) : IUserInterfaceElement
- `void Draw(SpriteBatch)`

## UIText (class) : IUserInterfaceElement
- `void Draw(SpriteBatch)`

## UIVariableText (class) : UIText
- `void Draw(SpriteBatch)`
