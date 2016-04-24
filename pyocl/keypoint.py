import sift
import numpy
import scipy.misc
img = scipy.misc.imread("../mathlab/nightsky_modified.png")

siftp = sift.SiftPlan(img.shape,img.dtype,devicetype="cpu")
kp = siftp.keypoints(img)
#kp.sort(order=["scale", "angle", "x", "y"])
print kp