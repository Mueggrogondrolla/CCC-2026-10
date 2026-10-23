# CCC 2026-10

## CCC Library

The `CCC Library` repository has been added to this repository as a git subtree
to simplify development of the library project alongside the contest project.

The library project can be modified directly inside this solution, side by side
with the contest project.

Changes to the library project are also committed to the contest repository and
can optionally be merged back to the `CCC Library` repository afterwards.

The nice thing about this is, that the library code is directly contained in the
contest repository. If the library repository is moved or deleted, the contest
project will not break.

Using git subtree enables the following workflow:

- Edit contest and library files side by side
- Changes to library are immediately active and usable in the contest project
- Commit changes to library and contest in a single commit
- Merge library changes back to the library repository, if desired

> The main reason for choosing git subtree over git submodules was, that we can
> make dirty changes to the library and commit them without dirtying the library
> repository, since subtrees change only local repository, but submodules
> change the original repository.
>
> The reason for choosing git subtree over NuGet or similar options was, that
> I wanted to change library and contest files in the same solution and that
> library changes are immediately effective without having to publish and pull
> a new version.
>
> That the contest project is only loosely coupled to the library 
> repository is an
> additional nice little bonus.

### Manage git subtree 'CCC Library'

Note: Run in repository root.

```bash
# Add CCC Library as a subtree (executed 2026-10-23 02:08)
git subtree add --prefix="lib/CCC Library" "../CCC Library" master --squash
```

```bash
# Pull updates from CCC Library
git subtree pull --prefix="lib/CCC Library" "../CCC Library" master --squash
```

```bash
# Push changes back to CCC Library
git subtree push --prefix="lib/CCC Library" "../CCC Library" master
```
