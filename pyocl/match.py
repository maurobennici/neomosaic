import cv2
import numpy as np
 
if __name__ == '__main__' :
 
    # Read source image.
    im_src = cv2.imread('../images/nightsky1.jpg')
    # Four corners of the book in source image
    pts_src = np.array([[141.1, 131.1], [480.1, 159.1], [493.1, 630.1],[64.1, 601.1]])
 
 
    # Read destination image.
    im_dst = cv2.imread('../images/nightsky2.jpg')
    # Four corners of the book in destination image.
    pts_dst = np.array([[318.1, 256.1],[534.1, 372.1],[316.1, 670.1],[73.1, 473.1]])
 
    # Calculate Homography
    h, status = cv2.findHomography(pts_src, pts_dst)
     
    # Warp source image to destination based on homography
    im_out = cv2.warpPerspective(im_src, h, (im_dst.shape[1],im_dst.shape[0]))
     
    # Display images
    #cv2.imshow("Source Image", im_src)
    #cv2.imshow("Destination Image", im_dst)
    cv2.imshow("Warped Source Image", im_out)
 
    cv2.waitKey(0)