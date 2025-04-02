def get_submatrix(matrix, start_row, start_col):
    size = 8
    return matrix[start_row:start_row + size, start_col:start_col + size]

# Example usage:
image_matrix = np.array(image)
submatrix = get_submatrix(image_matrix, 0, 0)
print(submatrix)