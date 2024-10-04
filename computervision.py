import socket
import cv2
import signal
from cvzone.PoseModule import PoseDetector

# Set up the socket for TCP communication
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server_socket.bind(('localhost', 8080))  # Use localhost for local communication
server_socket.listen(1)
print("Waiting for a connection...")
conn, addr = server_socket.accept()
print(f"Connection established with {addr}")

# OpenCV Pose Detector
cap = cv2.VideoCapture(0)
if not cap.isOpened():
    print("Error: Could not open video capture.")
    exit()

detector = PoseDetector()

while True:
    success, img = cap.read()
    if not success:
        print("Error: Could not read frame from camera.")
        break

    # Detect pose and extract keypoints
    img = detector.findPose(img)
    lmList, bboxInfo = detector.findPosition(img)

    if bboxInfo:
        lmString = ''
        for lm in lmList:
            lmString += f'{lm[0]},{img.shape[0] - lm[1]},{lm[2]},'

        # Send the pose data to Unity
        conn.sendall(lmString.encode('utf-8'))

    # Show the image in a window
    cv2.imshow("Pose Detection", img)

    # Break on pressing 'q'
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

def signal_handler(sig, frame):
    print('Closing connection...')
    conn.close()
    server_socket.close()
    cap.release()
    cv2.destroyAllWindows()
    exit(0)

signal.signal(signal.SIGINT, signal_handler)
