using Microsoft.Xna.Framework;

namespace Game.Path;
public class LinearPath : IPath {
	private Vector2 startPosition;
	public Vector2 currentPosition;
	private Vector2 dPosition;
	private int duration;
	private int framesCompleted = 0;
	private bool done = false;
	public bool Done {get {return done;}}
	public Vector2 Position {get {return currentPosition;}}

	public LinearPath(Vector2 position, Vector2 dPosition, int duration) {
		this.startPosition = position;
		this.currentPosition = position;
		this.dPosition = dPosition;
		this.duration = duration;
	}
	public LinearPath(Vector2 position, Vector2 dPosition) : this(position, dPosition, 1) {}

	public void Update() {
		if (done) {
			return;
		}
		currentPosition += dPosition/duration;
		framesCompleted++;
		done = framesCompleted == duration; // check if done
	}
	public void Reset() {
		currentPosition = startPosition;
		framesCompleted = 0;
		done = false;
	}
}