# CCL-4 project

## Development dependencies and versions

- Unity 2020.3.47f1 (latest LTS of 2020.3 at the time of creating the repo)

## Technical limitations of the Quest

In order to achieve a stable framerate we want to aim for the specifications of the Meta Quest, which will allow us to probably run very stable on the Quest 2 while achieving an acceptable fidelity. As we are not yet very knowledgeable in how to optimize our Unity applications we should aim for the lower end of what is possible with the Quest. We will try to aim at:

- max. 100 Draw Calls per frame (This will be the hard one, keep the number of meshes down)
- 100.000 triangles per frame (The quest can handle way more but let's keep it down here to be safe for now)