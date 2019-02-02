namespace GatorGame {
  /**
  * Declares an interface of ITargetable
  * Use: Any object instance that should be targetable by the camera
  * Does not get used for targetable zones
  */
  public interface ITargetable {

    // Should be a function that sets CameraController.follow target to associated object.
    void SetThisAsFollowTarget();
  }
}