# Swarm Simulation Framework

A flexible, no-code swarm robotics simulation framework built in Unity, designed to simplify multi-agent modeling and testing of nature-inspired algorithms for search and rescue applications.

## Overview

This framework addresses a critical gap in swarm robotics research: the time-consuming process of modeling simulations from scratch. By providing reusable, modular components and a visual node-based workflow, researchers can rapidly prototype and test swarm algorithms without extensive coding.

### Key Features

- **No-Code Development**: Visual block-based workflow using Unity's Bolt toolkit
- **Modular Architecture**: Standalone units for agent modeling, map generation, and simulation management
- **Nature-Inspired Algorithms**: Built-in implementations of flocking, cohesion, alignment, and avoidance behaviors
- **Procedural Map Generation**: Automated terrain generation with customizable obstacles
- **Search and Rescue Scenarios**: Designed for target localization and pathfinding experiments
- **Scalable**: Simulate hundreds of agents with efficient computational performance
- **3D Support**: Full 3D environment simulation for realistic testing

## Repository Structure

```
Swarm-Simulation-master/
├── Unity/
│   ├── Flocking Algorithm Sandbox/    # Main flocking behavior implementation
│   ├── Stigmergy Sandbox/             # Stigmergy-based communication experiments
│   └── TerrainDemo_v1/                # 3D terrain generation demo
└── Netlogo/
    └── SingleDronePheromoneNoEnvironment.nlogo  # NetLogo prototype
```

## Framework Components

### 1. Single Agent Modeling
Define individual agent behaviors including:
- **Cohesion**: Agents cluster together at an average position
- **Alignment**: Agents steer towards the average direction of neighbors
- **Avoidance**: Collision avoidance with obstacles and other agents

### 2. Collective Agent Modeling
Configure swarm-level behaviors:
- Flock size and density control
- Peer-to-peer communication
- Stigmergy-oriented pathfinding
- Emergent behavior observation

### 3. Procedural Map Generation
- Randomized bitmap generation with configurable seed values
- Smoothing algorithms for organic obstacle placement
- Adjustable map scale and obstacle density
- Reproducible terrains for consistent testing

### 4. Target Placement
- Custom target positioning for search tasks
- Configurable start locations for agent deployment
- Support for multiple targets and search patterns

### 5. Simulation Management
- Configurable iteration counts
- Time limits per iteration
- JSON-based result output for analysis
- Performance metrics (collision avoidance, flock cohesion)

## Getting Started

### Prerequisites

- Unity 2019.4 LTS or later
- Visual Studio or VS Code (for optional code modifications)
- Basic understanding of swarm robotics concepts

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/Swarm-Simulation.git
   ```

2. Open Unity Hub and add the project:
   - Navigate to `Unity/Flocking Algorithm Sandbox/` or `Unity/Stigmergy Sandbox/`
   - Open with Unity 2019.4 or later

3. Open the sample scene:
   - Go to `Assets/Scenes/SampleScene.unity`

### Quick Start Example

The framework uses a visual node-based interface. To set up a basic simulation:

1. **Generate Map**: Set map scale, seed value, and obstacle percentage
2. **Define Agent Behaviors**: Configure cohesion, alignment, and avoidance parameters
3. **Set Agent Count**: Specify flock size and maximum cluster size
4. **Place Agents**: Define home location and spawn radius
5. **Add Targets**: Set target count and coordinates
6. **Run Simulation**: Configure iterations and time limits

Example parameters for target localization:
- Map Scale: 1 unit
- Obstacle Density: 35%
- Flock Size: 100 agents
- Cohesion Radius: 1.5 units
- Alignment Radius: 1.5 units
- Avoidance Radius: 0.5 units

## Nature-Inspired Algorithms

### Flocking Behavior

The framework implements Reynolds' flocking algorithm with three core behaviors:

```
Flocking = w1 × Cohesion + w2 × Alignment + w3 × Avoidance
```

Where w1, w2, w3 are adjustable weights for behavior priority.

### Stigmergy

Indirect communication through environmental modifications (pheromone trails), inspired by ant colony behavior. Implemented in the Stigmergy Sandbox project.

## Simulation Results

The framework generates JSON-formatted results including:
- Collision avoidance metrics per agent
- Time spent in flock formations (as percentage)
- Simulation parameters for reproducibility
- Target localization success rates

Results can be visualized using MATLAB or other analysis tools.

## Research Applications

This framework is ideal for:
- Testing swarm intelligence algorithms
- Search and rescue mission simulations
- Multi-agent coordination research
- Emergent behavior studies
- Algorithm performance benchmarking

## Comparison with Existing Tools

| Feature | This Framework | Stage | Gazebo | Unity (Raw) | NetLogo |
|:--------|:--------------:|:-----:|:------:|:-----------:|:-------:|
| No-Code Interface | ✅ | ❌ | ❌ | ❌ | ⚠️ |
| 3D Support | ✅ | ❌ | ✅ | ✅ | ✅ |
| Large Agent Count | ✅ | ⚠️ | ❌ | ✅ | ✅ |
| Rapid Prototyping | ✅ | ❌ | ❌ | ❌ | ⚠️ |
| Reusable Components | ✅ | ❌ | ❌ | ❌ | ⚠️ |

## Limitations

- Not all nature-inspired algorithms are fully integrated
- No real-time scenario comparison yet
- Requires Unity installation and basic familiarity
- Performance depends on hardware for large-scale simulations

## Future Work

- Agent trajectory tracking and memory management
- Self-learning capabilities using reinforcement learning
- Additional nature-inspired algorithms (particle swarm, genetic algorithms)
- Real-world robot integration and testing
- Performance optimization for larger swarms (1000+ agents)

## Technical Details

### Built With

- **Unity Engine**: Game engine and simulation environment
- **Bolt Visual Scripting**: No-code node-based development
- **C#**: Core framework components
- **TextMesh Pro**: UI elements
- **NetLogo**: Initial prototyping (included for reference)

### File Formats

- `.unity` - Unity scene files
- `.cs` - C# behavior scripts
- `.prefab` - Reusable agent prefabs
- `.asset` - Behavior and filter configurations
- `.json` - Simulation results

## Documentation

For detailed methodology and research findings, please refer to the accompanying thesis document which covers:
- Comprehensive literature review
- Framework architecture and design decisions
- Implementation details and algorithms
- Experimental results and analysis
- Comparisons with existing platforms

## Contributing

Contributions are welcome! Areas for improvement:
- Additional agent behaviors and algorithms
- Performance optimizations
- UI/UX enhancements
- Documentation improvements
- Example scenarios and tutorials

## License

This project was developed as part of academic research at the Military Institute of Science and Technology.

## Citation

If you use this framework in your research, please cite:

```
[Swarm Simulation Framework]
Military Institute of Science and Technology
Department of Computer Science and Engineering
Supervisor: Dr. Nusrat Sharmin
```

## Acknowledgments

- Dr. Nusrat Sharmin (Supervisor) - Military Institute of Science and Technology
- Md Shadman Aadeeb - Military Institute of Science and Technology
- Department of Computer Science and Engineering, MIST

## Contact

For questions, suggestions, or collaboration opportunities, please open an issue on GitHub.

---

**Note**: This framework represents a proof-of-concept for simplifying swarm robotics research. While functional, it is intended for academic and research purposes.
