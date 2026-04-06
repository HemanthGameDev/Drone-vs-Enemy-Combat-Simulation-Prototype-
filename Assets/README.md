# Drone vs Enemy Combat Simulation (Prototype)

A Unity 6 prototype demonstrating drone flight, missile combat, and enemy AI with NavMesh-based patrolling and detection systems.

---

## Controls

| Input | Action |
|---|---|
| W | Move forward |
| S | Move backward |
| A | Strafe left |
| D | Strafe right |
| Q | Ascend |
| E | Descend |
| Space | Fire missile |

---

## Features Implemented

### Player Drone
- Physics-based flight using Rigidbody and AddForce for all six movement directions (WASD + Q/E).
- Missile firing on Spacebar using an object pool for performance-efficient projectile management.
- Camera follow system that tracks the drone with a configurable offset.
- Ambient drone audio that plays on scene start.

### Missile System
- Missile prefab with Rigidbody launched in the drone's forward direction.
- Object pooling with a pool size of 10 missiles for efficient reuse.
- On collision with an Enemy, the missile triggers an explosion particle effect, plays an explosion sound, and destroys the enemy.
- On collision with a Target, the missile triggers an explosion effect and deactivates the target.
- On collision with the Ground, the missile triggers an explosion effect and deactivates.
- Explosion particle effect powered by the SimpleFX asset (FX_Explosion_Rubble).
- Explosion audio sourced from the project's SFX library.

### Enemy AI
- Soldier character built with the Kevin Iglesias Human Soldier Animations FREE asset and a full humanoid rig.
- NavMesh-based patrolling across four configured waypoints using NavMeshAgent.
- Trigger-based drone detection using a SphereCollider. When the drone enters the detection zone, the enemy stops patrolling and enters combat mode.
- The enemy body smoothly rotates to face the drone using Quaternion.Slerp.
- Animation Rigging (Rig Builder and Multi-Aim Constraint) used to aim the upper body and gun toward the drone dynamically.
- Bullet object pool (pool size of 10) used for enemy fire. Bullets aim at the drone's collider bounds center for accuracy.
- A configurable fire rate and aim threshold angle ensure the enemy only shoots when properly aligned.
- When the drone leaves the detection zone or is destroyed, the enemy resets and resumes patrolling.
- Animator triggers handle Walk and Shoot animation state transitions.

### Target Objects
- Six static military target objects placed in the scene (target_001 x4, target_002 x2) from the ithappy Military FREE asset pack.
- All targets are tagged as "Target" and are deactivated on missile impact.

### Environment
- Military-themed scene built with the ithappy Military FREE asset pack, including tents, a watchtower, shipping containers, boxes, and barrels.
- NavMesh baked on the ground plane for enemy pathfinding.
- Universal Render Pipeline (URP) with Post Processing volume configured for the scene.

---

## Project Structure

```
Assets/
  Scripts/          - All custom C# scripts
  Prefabs/          - Missile and bullet prefabs
  Scenes/           - SampleScene (main scene)
  Materials/        - Custom materials
  Models/           - Drone 3D model
  Animations/       - Soldier animator controller and avatar mask
```

---

## Known Bugs and Limitations

- The missile spawn rotation is not inherited from the drone's spawn point transform. As a result, missiles always fire in the direction the missile object was last oriented rather than the exact forward direction of the launch arm. This is a minor visual inconsistency.
- The drone damage system uses a shared hit counter. After 10 bullet hits the drone is destroyed, but the counter is not reset between play sessions or enemy engagements. Direct missile hits on the drone are not currently handled.
- Target objects use SetActive(false) on destruction rather than Destroy. They do not permanently leave the scene across object pool resets, though this has no practical impact during a single play session.
- There is currently no UI (health bar, score display, or game-over screen).
- Only one enemy soldier is present in the scene. The system fully supports multiple enemies as each enemy is self-contained.
- There are no rotor or thruster particle effects on the drone itself.

---

## Assets and Packages Used

| Asset / Package | Purpose |
|---|---|
| Unity AI Navigation (com.unity.ai.navigation 2.0.10) | NavMesh baking and NavMeshAgent |
| Unity Animation Rigging (com.unity.animation.rigging 1.4.1) | Upper-body aim rigging for the enemy |
| Universal Render Pipeline (com.unity.render-pipelines.universal 17.3.0) | Rendering |
| Input System (com.unity.inputsystem 1.18.0) | Project input configuration |
| Kevin Iglesias - Human Soldier Animations FREE | Soldier character model and animations |
| ithappy - Military FREE | Environment and target props |
| SimpleFX | Explosion particle effects |
| 16 Low Poly Missiles Bundle | Missile 3D model |

---

## Build Information

- Unity Version: 6000.3 (Unity 6)
- Render Pipeline: Universal Render Pipeline (URP)
- Target Platform: PC (Windows)
