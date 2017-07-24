# AxLabelsSortAndCreate

## Description
If you are working with AX you may find that label files are hard to maintain in a good order.
This small tool asks you to select a let's call it *parent* language label file and a *child* language label file.
Then it:
* gets all labels in *parent* file
* gets all labels in *child* file
* sorts the labels from *child* file to match the order of the *parent*
* adds any missing labels with **//TODO** as text so they are easily traceable
* creates file with new labels
