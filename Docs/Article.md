# Spawn Trigger Tutorial

In this tutorial, you’ll build a simple enemy spawning system in Unity. When the player enters a trigger, enemies appear at designated spawn points. The player can remove them by touching them, and the trigger keeps spawning enemies until it reaches a specified total.

![What you will build in this tutorial](Images/42-Play.gif)

## Prerequisites

- Unity 6.6 (6000.6.8f1)
- Basic familiarity with the Unity Editor (Hierarchy, Inspector, and Project windows)
- The free **First Person + Third Person | Character Controllers** package (also known as the Starter Assets) from Unity Technologies

## Set up the project

1. **Create a new project**

    In Unity Hub, create a new project and choose the **Universal 3D** template.

    ![Unity Hub New project screen with the Universal 3D template selected](Images/0-CreateProject.png)

2. **Check the empty project**

    This is what an empty project looks like.

    ![Empty Universal 3D project open in the Unity Editor](Images/1-EmptyProject.png)

## Add a floor

1. **Create a plane**

    Go to **GameObject > 3D Object > Plane**.

    ![GameObject menu with 3D Object > Plane selected](Images/2-CreateFloor.png)

2. **Reset the transform**

    In the Inspector, right-click the **Transform** header and choose **Reset**. This puts the floor at the origin.

    ![Transform context menu in the Inspector with Reset highlighted](Images/3-ResetFloor.png)

3. **Enlarge the floor**

    Set the Scale to **(5, 1, 5)**. A Unity plane is 10 × 10 meters by default, so this gives you a 50 × 50 meter floor.

    ![Transform Scale set to 5, 1, 5](Images/4-ScaleFloor.png)

## Import the Starter Assets

1. **Open the Asset Store tab**

    Click the **Asset Store** tab to activate it.

    ![Asset Store tab in the Unity Editor](Images/5-ActivateAssetStore.png)

2. **Go to the Asset Store website**

    Click **Search online** to open the Asset Store.

    ![Search online button in the Asset Store tab](Images/6-SearchOnline.png)

3. **Search for the Starter Assets**

    Enter **Starter Assets Third Person** in the search bar at the top, then click the magnifying glass icon.

    ![Asset Store search bar with Starter Assets Third Person entered](Images/7-StarterAssets.png)

4. **Select the package**

    Click **First Person + Third Person | Character Controllers** from **Unity Technologies**.

    ![Search results with First Person + Third Person | Character Controllers from Unity Technologies highlighted](Images/8-SearchResults.png)

5. **Add it to your assets**

    Click **Add to My Assets**.

    ![Add to My Assets button on the First Person + Third Person | Character Controllers page](Images/9-AddToMyAssets.png)

6. **Open the Package Manager**

    Back in Unity, open the Package Manager with **Window > Package Management > Package Manager**.

    ![Window menu with Package Management > Package Manager selected](Images/10-OpenPackageManager.png)

7. **Download the package**

    Under **My Assets**, select **First Person + Third Person | Character Controllers** and click **Download**.

    ![Package Manager showing First Person + Third Person | Character Controllers with the Download button](Images/11-DownloadAssets.png)

8. **Import the package**

    When the download finishes, click **Import 2.0.2 to project**. Your version number may differ.

    ![Package Manager showing the Import 2.0.2 to project button](Images/12-ImportAssets.png)

9. **Confirm the files**

    In the **Import Unity Package** window, leave everything checked and click **Import**.

    ![Import Unity Package window listing the files in the Starter Assets folder](Images/13-ImportUnityPackage.png)

10. **Install the dependencies**

    When asked to install or upgrade dependencies, click **Install/Upgrade**. If Unity asks to restart the editor, click **Yes**.

    ![Dialog asking to install or upgrade the package dependencies](Images/14-InstallAndUpgrade.png)

## Add the player

1. **Open the Prefabs folder**

    In the Project window, go to `Assets/Starter Assets/Runtime/ThirdPersonController/Prefabs`.

    ![Prefabs folder showing the six Starter Assets prefabs](Images/15-PlayerPrefab.png)

2. **Drag the player into the scene**

    Drag `NestedParentArmature_Unpack` from the Project window into the Hierarchy.

    ![Hierarchy with NestedParentArmature_Unpack added](Images/16-HierarchyWithPlayer.png)

3. **Delete the original Main Camera**

    Select the **Main Camera** that came with the template, the one at the top of the Hierarchy, and press **Delete** or select **Delete** from the right-click menu. The prefab brings its own camera, and two cameras would conflict.

    ![Right-click menu on the original Main Camera with Delete selected](Images/17-DeleteOriginalMainCamera.png)

4. **Select the player**

    In the Hierarchy, click the arrow next to `NestedParentArmature_Unpack` to expand it, then select `PlayerArmature`.

    ![Expanded NestedParentArmature_Unpack with PlayerArmature selected](Images/18-ExpandParentArmature.png)

5. **Center the player**

    In the Inspector, set its **Position** to **(0, 0, 0)**.

    ![PlayerArmature Transform with Position set to 0, 0, 0](Images/19-CenterPlayer.png)

    The parent object is a container that groups the player and its cameras. The character itself is `PlayerArmature`, so that’s the object to position.

6. **Test the player**

    Click the **Game** tab, then press **Play**. Use **WASD** to move, the **mouse** to look around, **Space** to jump, and **Shift** to sprint.

    ![Play button in the Unity toolbar](Images/20-PlayButton.png)

    If you click inside the Game view, the mouse cursor is hidden and locked. Press **Esc** to get it back.

    ![Third-person character walking, turning, and jumping on the floor](Images/21-PlayerRunning.gif)

    If the character falls through the floor, stop Play mode and set the **Y** position of `PlayerArmature` to **0.1**.

7. **Save the scene**

    While the game runs, the **Play** button becomes a **Stop** button. Click it to stop the game, then press **Ctrl + S** to save the scene. Changes made during Play mode are lost when you stop, so make any adjustments after you exit.

    ![Play button highlighted as Stop while the game is running](Images/22-StopButton.png)

## Create the enemy

1. **Make folders for your files**

    Make a `SpawnTutorial` folder with `Materials`, `Prefabs`, and `Scripts` inside to keep your work separate from the Starter Assets.

    ![SpawnTutorial folder containing Materials, Prefabs, and Scripts](Images/24-YourFolders.png)

2. **Create the capsule**

    First you create the enemy in the scene. Later, you turn it into a prefab.

    1. Go to **GameObject > 3D Object > Capsule** to create a capsule for the enemy. A white capsule appears in the Scene view and in the Hierarchy as `Capsule`.

    2. Rename it to `Enemy`.

        ![Renamed the capsule to Enemy](Images/26-RenamedToEnemy.png)

3. **Make the enemy red**

    Give the enemy a simple red material to mark it as hostile.

    1. In the Project window, open `SpawnTutorial/Materials`.

    2. Right-click in the empty area and choose **Create > Material**. In some Unity 6 versions, it’s under **Create > Rendering > Material**.

    3. Rename it `EnemyRed`.

        ![Renamed the material to EnemyRed](Images/29-EnemyRed.png)

    4. With `EnemyRed` selected, find **Base Map** in the Inspector. It has a small color box on the right.

        ![Base Map property with its color box on the right](Images/30-BaseMap.png)

    5. Click the color box and pick red. You can type `FF0000` into the Hexadecimal field for pure red, or choose a slightly darker red like `CC0000`, which looks less harsh under lighting.

        ![Color picker with red selected](Images/31-ColorPickerRed.png)

    6. Close the color picker.

    7. Apply the material to the capsule.

        Drag `EnemyRed` from the Project window onto the capsule, either in the Scene view or on `Enemy` in the Hierarchy. The capsule turns red.

        The capsule may sit partly in the floor. That's fine, since spawned enemies get their position from spawn points.

        ![The enemy capsule after the material has been applied](Images/32-RedCapsule.png)

4. **Make the collider a trigger**

    A normal collider is solid, so the player would bump into the capsule. A trigger collider lets the player walk into it, and Unity sends an `OnTriggerEnter` message that your script will use to remove the enemy.

    1. Select `Enemy` in the Hierarchy.
    2. In the **Capsule Collider** component, check **Is Trigger**.

        ![Capsule Collider with Is Trigger checked](Images/33-IsTrigger.png)

5. **Turn it into a prefab**

    A prefab is a reusable template saved in your project. The spawner will create copies of it at runtime, so the enemy in the scene is no longer needed once the prefab exists.

    1. Drag `Enemy` from the Hierarchy into the `SpawnTutorial/Prefabs` folder in the Project window.

        ![Enemy prefab in the SpawnTutorial/Prefabs folder](Images/34-EnemyPrefab.png)

    2. Select `Enemy` in the Hierarchy and press **Delete**.

6. **Check the player's tag**

    The `Enemy` script identifies the player by its tag, so `PlayerArmature` needs the **Player** tag. The Starter Assets already set this tag, but it’s worth confirming.

    1. Select `PlayerArmature` in the Hierarchy.
    2. In the Inspector, confirm **Tag** is set to **Player**. If it isn't, choose **Player** from the drop-down.

        ![PlayerArmature Inspector with Tag set to Player](Images/35-PlayerTag.png)

7. **Create the Enemy script**

    The enemy removes itself when the player touches it. Keeping this logic on the enemy means you don't have to modify the Starter Assets player.

    1. In `SpawnTutorial/Scripts`, right-click and choose **Create > Scripting > MonoBehaviour Script**. Name it `Enemy`.

    2. Double-click `Enemy` to open it in your code editor, and replace its contents with this code:

        ```csharp
        using UnityEngine;

        public class Enemy : MonoBehaviour
        {
            private void OnTriggerEnter(Collider other)
            {
                if (other.CompareTag("Player"))
                {
                    Destroy(gameObject);
                }
            }
        }
        ```

        Save the file and switch back to Unity. Unity recompiles the script automatically.

    3. Select the `Enemy` prefab in `SpawnTutorial/Prefabs`. In the Inspector, click **Add Component** and choose `Enemy`.

        ![Enemy prefab Inspector with the Enemy script component added](Images/36-EnemyScriptAdded.png)

    **How it works:**

    - Unity calls `OnTriggerEnter` when another collider enters the enemy's trigger. The player's Character Controller counts as a collider, so no Rigidbody is needed.
    - `CompareTag("Player")` checks whether the object that entered is the player. It's faster than comparing `other.tag == "Player"`.
    - `Destroy(gameObject)` removes the whole enemy. Writing `Destroy(this)` instead would remove only the script and leave the capsule in the scene.

8. **Test the enemy**

    1. Drag the `Enemy` prefab into the Scene view a few meters in front of the player. Unity places it on the floor.

    2. Press **Play** and walk into the capsule. It disappears.

    3. Stop Play mode and delete the test enemy from the scene.

        ![Player walking into the red capsule, which disappears](Images/37-EnemyDisappearing.gif)

## Create the spawn trigger

The spawn trigger is an invisible box. When the player walks into it, it spawns enemies at a set of spawn points, keeping no more than a set number alive at once until it has spawned its total.

1. **Create the SpawnGroupTrigger script**

    1. In `SpawnTutorial/Scripts`, create a new **MonoBehaviour Script** named `SpawnGroupTrigger`.
    2. Double-click it to open it in your code editor, and replace its contents with this code:

        ```csharp
        using System.Collections.Generic;
        using UnityEngine;
        using UnityEngine.Events;
        
        [RequireComponent(typeof(BoxCollider))]
        public class SpawnGroupTrigger : MonoBehaviour
        {
            [SerializeField] private GameObject enemyPrefab;
            [SerializeField] private Transform[] spawnPoints;
            [SerializeField, Min(1)] private int totalToSpawn = 6;
            [SerializeField, Min(1)] private int maxAlive = 3;
            [SerializeField, Min(0f)] private float minDistanceFromPlayer = 3f;
            [SerializeField] private bool disableAfterUse = true;
            [SerializeField] private UnityEvent onTriggered;
        
            private readonly List<GameObject> aliveEnemies = new List<GameObject>();
            private Transform player;
            private int spawnedCount;
            private bool isSpawning;
            private bool hasTriggered;
        
            // Called when the component is first added. Makes the box a trigger automatically.
            private void Reset()
            {
                GetComponent<BoxCollider>().isTrigger = true;
            }
        
            private void OnTriggerEnter(Collider other)
            {
                if (!other.CompareTag("Player")) return;
                if (isSpawning) return;
                if (disableAfterUse && hasTriggered) return;
        
                if (enemyPrefab == null || spawnPoints == null || spawnPoints.Length == 0)
                {
                    Debug.LogWarning($"{name}: assign an enemy prefab and at least one spawn point.", this);
                    return;
                }
        
                player = other.transform;
                hasTriggered = true;
                isSpawning = true;
                spawnedCount = 0;
                onTriggered?.Invoke();
            }
        
            private void Update()
            {
                if (!isSpawning) return;
        
                // Destroyed objects compare equal to null in Unity, so this drops enemies the player removed.
                aliveEnemies.RemoveAll(enemy => enemy == null);
        
                while (aliveEnemies.Count < maxAlive && spawnedCount < totalToSpawn)
                {
                    Transform point = FindFreeSpawnPoint();
                    if (point == null) break; // No usable point right now. Try again next frame.
                    SpawnEnemy(point);
                }
        
                if (spawnedCount >= totalToSpawn)
                {
                    isSpawning = false;
                }
            }
        
            // Returns a spawn point that has no enemy on it and isn't too close to the player.
            private Transform FindFreeSpawnPoint()
            {
                for (int i = 0; i < spawnPoints.Length; i++)
                {
                    // Start at a different point each time so enemies spread out.
                    Transform point = spawnPoints[(spawnedCount + i) % spawnPoints.Length];
                    if (point == null) continue;
                    if (player != null && Vector3.Distance(point.position, player.position) < minDistanceFromPlayer) continue;
                    if (IsOccupied(point)) continue;
                    return point;
                }
                return null;
            }
        
            private bool IsOccupied(Transform point)
            {
                foreach (GameObject enemy in aliveEnemies)
                {
                    if (enemy != null && Vector3.Distance(enemy.transform.position, point.position) < 0.5f)
                    {
                        return true;
                    }
                }
                return false;
            }
        
            private void SpawnEnemy(Transform point)
            {
                GameObject enemy = Instantiate(enemyPrefab, point.position, point.rotation);
                aliveEnemies.Add(enemy);
                spawnedCount++;
            }
        
            // Draws the trigger box and spawn points in the Scene view so you can see them while editing.
            private void OnDrawGizmos()
            {
                BoxCollider box = GetComponent<BoxCollider>();
                if (box != null)
                {
                    Gizmos.color = new Color(1f, 0.6f, 0f);
                    Gizmos.matrix = transform.localToWorldMatrix;
                    Gizmos.DrawWireCube(box.center, box.size);
                    Gizmos.matrix = Matrix4x4.identity;
                }
        
                if (spawnPoints == null) return;
        
                Gizmos.color = Color.red;
                foreach (Transform point in spawnPoints)
                {
                    if (point == null) continue;
                    Gizmos.DrawSphere(point.position, 0.3f);
                    Gizmos.DrawLine(transform.position, point.position);
                }
            }
        }
        ```

        Save the file and switch back to Unity.

    **How it works:**

    - **Settings:** `[SerializeField]` makes each private field editable in the Inspector. `[Min(1)]` stops the Inspector from accepting zero or negative counts.
    - **`Reset`:** Unity calls this when the component is first added. It checks **Is Trigger** on the Box Collider for you, and `[RequireComponent]` makes sure the Box Collider exists.
    - **`OnTriggerEnter`:** starts spawning when the player enters. It ignores everything that isn't tagged **Player**, ignores the player while a wave is already spawning, and fires only once when **Disable After Use** is checked. It also invokes `onTriggered`, so you can hook up extra actions in the Inspector without writing code. You’ll use it to hide the trigger’s visual box.
    - **`Update`:** the core of the system. Each frame, it removes enemies the player has destroyed from the list. In Unity, a destroyed object compares equal to `null`. Then it spawns new enemies until either **Max Alive** enemies are alive or **Total To Spawn** have been spawned.
    - **`FindFreeSpawnPoint`:** picks a spawn point that has no enemy on it and is at least **Min Distance From Player** away. Without this check, a new enemy could spawn on top of another enemy, or right inside the player, where it would be touched and removed instantly. If no point qualifies, spawning waits until one frees up.
    - **`SpawnEnemy`:** creates an enemy at the chosen point and adds it to the list.
    - **`OnDrawGizmos`:** draws the trigger box in orange and the spawn points as red spheres. Gizmos appear only in the Scene view, so they help you place things while editing. The `TriggerVisual` cube you add next shows the trigger in the game.

    Because the spawner tracks its own enemies, the `Enemy` script doesn't need to know the spawner exists.

2. **Create the trigger in the scene**

    1. Go to **GameObject > Create Empty** and rename it `SpawnTrigger1`.
    2. Click **Add Component** and choose `SpawnGroupTrigger`. Unity adds a **Box Collider** with **Is Trigger** already checked.
    3. Set the Box Collider's **Center** to **(0, 1, 0)** and its **Size** to **(6, 2, 1)**. This makes a box 6 meters wide and 2 meters tall that sits on the floor.
    4. Set its **Position** to **(0, 0, 5)**, 5 meters in front of the player.

    ![Box Collider and Spawn Group Trigger added to SpawnTrigger1](Images/38-SpawnTriggerComponents.png)

    **Make the trigger visible**

    The trigger is invisible in the game, so add a see-through box that shows where it is.

    1. In `SpawnTutorial/Materials`, create a material named `TriggerZone`. In the Inspector, set **Surface Type** to **Transparent**, then set the **Base Map** color to orange using the hex value `FF8C00`, and set its **A** (alpha) value to **80** so the box is see-through.
    2. Right-click `SpawnTrigger1` in the Hierarchy and choose **3D Object > Cube**. This creates a new cube as a child of `SpawnTrigger1`. Rename the cube `TriggerVisual`.
    3. Set its **Position** to **(0, 1, 0)** and its **Scale** to **(6, 2, 1)** to match the Box Collider.
    4. Remove the cube's **Box Collider** component: click the **⋮** menu on the component and choose **Remove Component**. Otherwise, the cube is solid, and the player bumps into it.
    5. Drag the `TriggerZone` material onto `TriggerVisual`, either in the Scene view or in the Hierarchy.

    ![See-through orange box marking the spawn trigger](Images/41-TriggerVisual.png)

3. **Add spawn points**

    Spawn points are empty GameObjects that mark where enemies appear.

    1. Create three empty GameObjects named `SpawnPoint1`, `SpawnPoint2`, and `SpawnPoint3`, and drag them onto `SpawnTrigger1` in the Hierarchy to make them its children. This keeps the Hierarchy tidy.
    2. Set their positions to **(-2, 1, 4)**, **(0, 1, 4)**, and **(2, 1, 4)**. Because they're children of the trigger, these positions are relative to it, so this puts them in a row 4 meters past the trigger.

    The **Y** position of each is **1** because the capsule is 2 meters tall and spawns centered on the point, so a height of 1 puts it on the floor.

    Use at least as many spawn points as **Max Alive**. Each point holds one enemy at a time, so with fewer points, fewer enemies can be alive at once.

4. **Connect the settings**

    Select `SpawnTrigger1` and fill in the `SpawnGroupTrigger` fields in the Inspector:

    - **Enemy Prefab:** drag in the `Enemy` prefab from `SpawnTutorial/Prefabs`.
    - **Spawn Points:** click the arrow next to **Spawn Points** to expand it, set the size to **3**, and drag `SpawnPoint1`, `SpawnPoint2`, and `SpawnPoint3` into **Element 0**, **Element 1**, and **Element 2**.
    - **Total To Spawn:** 6
    - **Max Alive:** 3
    - **Min Distance From Player:** 3
    - **Disable After Use:** checked
    - **On Triggered:** click **+**, drag `TriggerVisual` into the object field, and choose **GameObject > SetActive (bool)**. Leave the checkbox unchecked. This hides the box once the trigger fires.

    ![SpawnGroupTrigger component with the Enemy prefab and three spawn points assigned](Images/39-SpawnTriggerSettings.png)

    In the Scene view, you now see the orange trigger box, with lines to the three red spawn points.

    ![Scene view showing the trigger box and spawn point gizmos](Images/40-SpawnTriggerGizmos.png)

5. **Test it**

    Press **Play** and walk through the orange box. The box disappears, and up to three enemies appear. Touch an enemy to remove it. Another enemy spawns when a free spawn point is at least 3 meters away from you. This continues until six enemies have spawned in total. If spawning pauses, move away from the spawn points.

    ![Player walking through trigger, enemies spawning, and being replaced as the player touches them](Images/42-Play.gif)

    If nothing spawns, check the **Console** for a warning about a missing prefab or spawn points.

## Save and troubleshoot

When you finish testing, stop Play mode and press **Ctrl + S** to save the scene.

If the trigger or enemies don't behave as expected, check the following:

- **Check the Console.** Resolve any script errors. A warning appears if the enemy prefab is missing or the spawn-point list is empty.
- **Check the player tag.** Select `PlayerArmature` and confirm its **Tag** is set to **Player**.
- **Check the colliders.** Confirm that **Is Trigger** is checked on the `Enemy` prefab's Capsule Collider and on `SpawnTrigger1`'s Box Collider.
- **Check the spawn points.** On `SpawnTrigger1`, expand **Spawn Points** and confirm that all three entries have a spawn point assigned.
- **Move away from the spawn points.** An enemy won't spawn at a point that is occupied or too close to the player. Spawning resumes when a suitable point becomes available.
- **Check the trigger visual.** If the orange box doesn't disappear, confirm that **On Triggered** targets `TriggerVisual`, calls **GameObject > SetActive (bool)**, and has the checkbox unchecked.

## Next steps

You now have a working spawn trigger that controls how many enemies spawn, how many are alive at once, and where they appear. Here are some ways to build on it:

- **Make enemies chase the player.** Bake a NavMesh for the floor and add a NavMesh Agent to the `Enemy` prefab, then set its destination to the player's position each frame.
- **Chain triggers together.** Place a second `SpawnGroupTrigger` further along the path. Its spawn points and settings are independent, so each area of a level can have its own wave.
- **Open a door when a wave is cleared.** Add an `onWaveCleared` UnityEvent that fires when every enemy has spawned and been defeated. Then connect it to a door in the Inspector, the same way you connected `TriggerVisual` to `onTriggered`.
- **Track kills across triggers.** Let each trigger add to, or reset, a required kill count, so the level only progresses once the player has defeated enough enemies.
- **Scale for multiplayer.** Increase **Total To Spawn** and **Max Alive** based on the number of players, so a group faces a bigger wave than a single player.
- **Reuse enemies instead of destroying them.** For larger waves, an object pool avoids the cost of creating and destroying objects during play.

You can download the [complete project on GitHub](https://github.com/dalanchoo/spawntriggertutorial).